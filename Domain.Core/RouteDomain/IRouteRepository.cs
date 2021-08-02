using System.Collections.Generic;

namespace Domain.Core
{
    public interface IRouteRepository
    {
        void AddRoute(Route route);

        ICollection<Route> GetAllRoutes();

        void RemoveRoutes(IEnumerable<Route> routes);

        void RemoveRoute(Route route);
    }
}