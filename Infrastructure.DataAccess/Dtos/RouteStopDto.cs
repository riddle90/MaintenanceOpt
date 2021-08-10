using System;

namespace Infrastructure.DataAccess.Dtos
{
    public class RouteStopDto
    {
        public Guid RouteID { get; set; }

        public long CustomerId { get; set; }

        public int StopSequence { get; set; }

        public TimeSpan ETA { get; set; }
        
        public TimeSpan ETD { get; set; }

    }
}