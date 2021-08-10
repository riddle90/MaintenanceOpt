using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Domain.Core
{
    public class Route
    {
        public Guid RouteId { get; }
        
        private List<Stop> Stops { get; }
        
        private RouteDetails RouteDetails { get; }
        

        public Route(Guid routeId, List<Stop> stops, RouteDetails routeDetails)
        {
            RouteId = routeId;
            Stops = stops;
            RouteDetails = routeDetails;
        }

        public IEnumerable<Stop> GetStops()
        {
            return this.Stops;
        }

        public int GetStopArrivalTime(Stop stop)
        {
            return this.RouteDetails.ArrivalTime[stop];
        }

        public int GetTotalMiles()
        {
            return this.RouteDetails.TotalKms;
        }
    }
}