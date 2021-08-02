using System;
using System.Collections.Generic;
using Domain.Core;

namespace Infrastructure.Repository
{
    public class RouteRepository : IRouteRepository
    {
        public RouteRepository()
        {
            this.OptimizedRoutes = new Dictionary<Guid, Route>();
        }

        public Dictionary<Guid, Route> OptimizedRoutes { get; set; }


        public void AddRoute(Route route)
        {
            this.OptimizedRoutes.Add(route.routeId, route);
        }

        public ICollection<Route> GetAllRoutes()
        {
            return this.OptimizedRoutes.Values;
        }

        public void RemoveRoutes(IEnumerable<Route> routes)
        {
            foreach (var route in routes)
            {
                this.RemoveRoute(route);
            }
        }

        public void RemoveRoute(Route route)
        {
            this.OptimizedRoutes.Remove(route.routeId);
        }
    }
}