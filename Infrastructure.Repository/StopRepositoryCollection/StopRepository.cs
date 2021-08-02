using System.Collections.Generic;
using Domain.Core;

namespace Infrastructure.Repository.StopRepositoryCollection
{
    public class StopRepository : IStopRepository
    {
        public StopRepository()
        {
            this._stops = new List<Stop>();
        }

        private List<Stop> _stops;

        public void AddStop(Stop stop)
        {
            this._stops.Add(stop);
        }

        public ICollection<Stop> GetStops()
        {
            return this._stops;
        }
    }
}