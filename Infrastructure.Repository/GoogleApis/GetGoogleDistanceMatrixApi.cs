using System.Collections.Generic;
using System.Linq;
using Domain.Core;
using Domain.Core.DistanceMatrixDomain;
using Domain.Core.LocationDomain;
using Google.Maps;
using Google.Maps.DistanceMatrix;
using Stop = Domain.Core.Stop;

namespace Infrastructure.Repository.GoogleApis
{
    public class GetGoogleDistanceMatrixApi : IGetDistanceMatrixApi
    {
        private readonly IStopRepository _stopRepository;
        private readonly IDistanceMatrixRepository _distanceMatrixRepository;

        public GetGoogleDistanceMatrixApi(IStopRepository stopRepository, IDistanceMatrixRepository distanceMatrixRepository)
        {
            _stopRepository = stopRepository;
            _distanceMatrixRepository = distanceMatrixRepository;
        }

        public void GetMatrix(string apiKey)
        {
            GoogleSigned.AssignAllServices(new GoogleSigned(apiKey));            
            var distanceMatrixRequest = new DistanceMatrixRequest();
            distanceMatrixRequest.Mode = TravelMode.driving;
            distanceMatrixRequest.Units = Units.metric;

            var stops = _stopRepository.GetStops();
            var addressToStopDictionary = new Dictionary<string, Stop>();
            foreach (var stop in stops)
            {
                var address = $"{stop.Address}+{stop.City}+{stop.Zipcode}";
                addressToStopDictionary.Add(address, stop);
                distanceMatrixRequest.AddOrigin(new Location(address));
                distanceMatrixRequest.AddDestination(new Location(address));
            }

            try
            {
                var response = new DistanceMatrixService().GetResponse(distanceMatrixRequest);
                for (int i = 0; i < response.OriginAddresses.Length; i++)
                {
                    var originStop = stops.ElementAt(i);
                    for (int j = 0; j < response.DestinationAddresses.Length; j++)
                    {
                        var destinationStop = stops.ElementAt(j);
                        float distance = 0;
                        float duration = 0;

                        if (i != j)
                        {
                            distance = (float) response.Rows[i].Elements[j].distance.Value;
                            duration = (float) response.Rows[i].Elements[j].distance.Value;
                        }
                        
                        var distanceInfo = new DistanceInfo(distance, duration);
                        _distanceMatrixRepository.Add(originStop.CustomerId, destinationStop.CustomerId, distanceInfo);
                    }
                }

            }
            catch
            {
                
            }

        }
    }
}