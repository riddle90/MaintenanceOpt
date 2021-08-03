using System;

namespace Infrastructure.DataAccess.Dtos
{
    public class RouteStopDto
    {
        public Guid RouteID { get; set; }

        public int CustomerId { get; set; }

        public int StopSequence { get; set; }

        public TimeSpan ETA { get; set; }
        
        public TimeSpan ETD { get; set; }

    }
}