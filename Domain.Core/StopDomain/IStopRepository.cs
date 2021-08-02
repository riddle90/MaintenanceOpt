using System.Collections.Generic;

namespace Domain.Core
{
    public interface IStopRepository
    {
        void AddStop(Stop stop);

        ICollection<Stop> GetStops();
    }
}