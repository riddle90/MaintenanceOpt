using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Core.DistanceMatrixDomain;
using Domain.Core.LocationDomain;
using Google.Maps;
using Google.Maps.DistanceMatrix;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Stop = Domain.Core.Stop;

namespace Infrastructure.Repository.GoogleApis
{
    public class GetGoogleDistanceMatrixApi : IGetDistanceMatrixApi
    {
        private readonly IStopRepository _stopRepository;
        private readonly IDistanceMatrixRepository _distanceMatrixRepository;
        private readonly ILogger _logger;

        public GetGoogleDistanceMatrixApi(IStopRepository stopRepository, IDistanceMatrixRepository distanceMatrixRepository,
            ILoggerFactory loggerFactory)
        {
            _stopRepository = stopRepository;
            _distanceMatrixRepository = distanceMatrixRepository;
            _logger = loggerFactory.CreateLogger(this.GetType().Name);
        }

        public async Task GetMatrix(string apiKey)
        {
            GoogleSigned.AssignAllServices(new GoogleSigned(apiKey));            
            
            var stops = _stopRepository.GetStops();
            for (int j = 0; j < stops.Count; j += 10)
            {
                var distanceMatrixRequests = new List<DistanceMatrixRequest>();
                

                await Task.Delay(1000);
                _logger.LogDebug($"Starting New Origin Address {j}");
                var originStops = new List<Stop>();
                var originLocations = new List<Location>();
                for (int k = 0; k < 10; k++)
                {
                    
                    var origStop = stops.ElementAt(j + k);
                    originStops.Add(origStop);
                    var originAddress = $"{origStop.Address}+{origStop.City}+{origStop.Zipcode}";
                    originLocations.Add(new Location(originAddress));
                    

                }

                var allRequests = new Dictionary<int, DistanceMatrixRequest>();
                var allDestinationStops = new Dictionary<int, List<Stop>>();
                var destinationStops = new List<Stop>();
                var destinationLocations = new List<Location>();
                int key = 0;


                for (int i = 0; i < stops.Count; i++)
                {
                    var destStop = stops.ElementAt(i);
                    var destinationAddress = $"{destStop.Address}+{destStop.City}+{destStop.Zipcode}";
                    destinationLocations.Add(new Location(destinationAddress));
                    destinationStops.Add(destStop);

                    if (i > 0 && (i+1) % 10 == 0)
                    {
                        //await CallApi(distanceMatrixRequest, destinationStops, originStops);
                        allRequests.Add(key, GetDistanceMatrixRequestion(originLocations, destinationLocations));
                        allDestinationStops.Add(key, destinationStops);
                        key++;
                        destinationStops = new List<Stop>();
                        destinationLocations = new List<Location>();
                        _logger.LogDebug($"Done with {i+1} destinations");
                    }
                }

                if (allDestinationStops.Any())
                {
                    allDestinationStops.Add(key, destinationStops);
                    allRequests.Add(key, GetDistanceMatrixRequestion(originLocations, destinationLocations));
                    //await this.CallApi(distanceMatrixRequest, AlldestinationStops, originStops);
                }

                await CallParallelApi(allRequests, allDestinationStops, originStops);
            }
        }

        private async Task CallParallelApi(Dictionary<int,DistanceMatrixRequest> allRequests, 
            Dictionary<int,List<Stop>> allDestinationStops, List<Stop> originStops)
        {

            var policy = GetPolicy();
            var responseTasks =  allRequests.Select(kvp=> policy.ExecuteAsync(async ()=> 
            {
                var response = await new DistanceMatrixService().GetResponseAsync(allRequests[kvp.Key]);
                return (allDestinationStops[kvp.Key], response);

            }));

            var responses = await Task.WhenAll(responseTasks);

            foreach (var tuple in responses)
            {
                UpdateDistanceMatrix(tuple.Item2, tuple.Item1, originStops);
            }

        }

        private DistanceMatrixRequest GetDistanceMatrixRequestion(List<Location> originLocations, List<Location> destinationLocations)
        {
            var distanceMatrixRequest = new DistanceMatrixRequest();
            distanceMatrixRequest.Mode = TravelMode.driving;
            distanceMatrixRequest.Units = Units.metric;
            distanceMatrixRequest.WaypointsOrigin = originLocations;
            distanceMatrixRequest.WaypointsDestination = destinationLocations;
            return distanceMatrixRequest;
        }

        private void UpdateDistanceMatrix(DistanceMatrixResponse response, List<Stop> destinationStops, List<Stop> originStops)
        {
            try
            {
                for (int i = 0; i < response.OriginAddresses.Length; i++)
                {
                    var originStop = originStops.ElementAt(i);
                    for (int j = 0; j < response.DestinationAddresses.Length; j++)
                    {
                        var destinationStop = destinationStops.ElementAt(j);
                        float distance = 0;
                        float duration = 0;

                        if (originStop != destinationStop)
                        {
                            if (response.Rows[i].Elements[j].Status == ServiceResponseStatus.Ok)
                            {
                                distance = (float) response.Rows[i].Elements[j].distance.Value;
                                duration = (float) response.Rows[i].Elements[j].distance.Value / 60;
                            }

                        }
                        var distanceInfo = new DistanceInfo(distance, duration);
                        _distanceMatrixRepository.Add(originStop.CustomerId, destinationStop.CustomerId, distanceInfo);
                    }
                }

            }
            catch(Exception e)
            {
                _logger.LogError("Distance Matrix Api failed");
                throw new Exception("Google API failed");
            }
        }

        private AsyncRetryPolicy<(List<Stop>, DistanceMatrixResponse)> GetPolicy()
        {
            var policy = Policy
                .Handle<Exception>()
                .OrResult<(List<Stop>, DistanceMatrixResponse)>(message => !(message.Item2.Status == ServiceResponseStatus.Ok))
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(4),
                    TimeSpan.FromSeconds(8),
                    TimeSpan.FromSeconds(16),
                    TimeSpan.FromSeconds(32),
                }, (results, retryTime, retryCount, context) =>
                {
                    // Log Warn saying a retry was required.
                    _logger.LogError(
                        $"Retry {retryCount} due to {results.Result.Item2?.Status.ToString() ?? results.Exception.Message} , retry after {retryTime.TotalSeconds} seconds");

                });

            return policy;
        }
    }
}