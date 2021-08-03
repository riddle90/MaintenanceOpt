using Domain.Core;
using Infrastructure.Repository;
using Infrastructure.Repository.StopRepositoryCollection;
using SimpleInjector;

namespace RoutingEngine.Webapp
{
    public static class RoutingEngineBootStrapper
    {
        public static void InitializeContainer(Container container)
        {
            InitializeRepository(container);
            
        }

        private static void InitializeRepository(Container container)
        {
            container.Register<IRouteRepository, RouteRepository>();
            container.Register<IStopRepository, StopRepository>();
        }
    }
}