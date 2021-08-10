using System.Collections.Generic;

namespace Domain.Core
{
    public class RouteDetails
    {
        public Dictionary<Stop, int> ArrivalTime{ get; }
        
        public int TotalKms { get; }
        
        public  FeasibilityStatus Status { get; }

        public RouteDetails(Dictionary<Stop, int> arrivalTime, int totalKms, FeasibilityStatus status)
        {
            ArrivalTime = arrivalTime;
            TotalKms = totalKms;
            Status = status;
        }
        
    }
}