using System.Collections.Generic;

namespace Domain.Core
{
    public class RouteDetails
    {
        public Dictionary<Stop, int> ArrivalTime{ get; }
        
        public int TotalMiles { get; }
        
        public  FeasibilityStatus Status { get; }

        public RouteDetails(Dictionary<Stop, int> arrivalTime, int totalMiles, FeasibilityStatus status)
        {
            ArrivalTime = arrivalTime;
            TotalMiles = totalMiles;
            Status = status;
        }
        
    }
}