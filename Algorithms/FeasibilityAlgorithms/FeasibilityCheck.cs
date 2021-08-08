using System.Collections.Generic;
using Domain.Core;

namespace Algorithms.FeasibilityAlgorithms
{
    public class FeasibilityCheck : IFeasibilityCheck
    {
        public RouteDetails CheckFeasibility(List<Stop> stops)
        {
            var arrivalTime = new Dictionary<Stop, int>();
            var totalMiles = 0;
            var status = FeasibilityStatus.Feasible;

            return new RouteDetails(arrivalTime, totalMiles, status);
        }
    }
}