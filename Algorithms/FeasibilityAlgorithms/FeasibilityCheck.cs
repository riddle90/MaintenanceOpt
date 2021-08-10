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
        public RouteDetails CheckFeasibility(List<Stop> stops, int routeStartTime)
        {
            var arrivalTimes = new Dictionary<Stop, int>();
            var totalKms = 0.0;
            var status = FeasibilityStatus.Feasible;

            
            var prevDeptTime = routeStartTime;
            long prevCustomerID = 0;
            foreach (var stop in stops)
            {
                var travelTime = (int) DistanceMatrixRepository.GetTime(prevCustomerID, stop.CustomerId);
                var distance = DistanceMatrixRepository.GetDistance(prevCustomerID, stop.CustomerId);
                
                var arrivalTime = prevDeptTime + travelTime;
                arrivalTimes.Add(stop,arrivalTime);
                if (arrivalTime > stop.TimeWindow.EndTime)
                    status = FeasibilityStatus.Infeasible;
                
                prevDeptTime = arrivalTime + 60;
                totalKms += distance/1000;
                prevCustomerID = stop.CustomerId;
            }

            return new RouteDetails(arrivalTimes, (int)totalKms, status);
        }
    }
}