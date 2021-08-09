using System.Collections.Generic;
using Domain.Core;
using Domain.Core.DistanceMatrixDomain;

namespace Algorithms.FeasibilityAlgorithms
{
    public class FeasibilityCheck : IFeasibilityCheck
    {
        public IDistanceMatrixRepository DistanceMatrixRepository { get; }

        public FeasibilityCheck(IDistanceMatrixRepository distanceMatrixRepository)
        {
            DistanceMatrixRepository = distanceMatrixRepository;
        }
        public RouteDetails CheckFeasibility(List<Stop> stops)
        {
            var arrivalTimes = new Dictionary<Stop, int>();
            var totalMiles = 0;
            var status = FeasibilityStatus.Feasible;

            
            var prevDeptTime = 0;
            long prevCustomerID = 0;
            foreach (var stop in stops)
            {
                var travelTime = (int) DistanceMatrixRepository.GetTime(prevCustomerID, stop.CustomerId);
                var distance = (int) DistanceMatrixRepository.GetDistance(prevCustomerID, stop.CustomerId);
                
                var arrivalTime = prevDeptTime + travelTime;
                arrivalTimes.Add(stop,arrivalTime);
                prevDeptTime = arrivalTime + 60;
                totalMiles += distance;
                prevCustomerID = stop.CustomerId;
            }

            return new RouteDetails(arrivalTimes, totalMiles, status);
        }
    }
}