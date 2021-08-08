using Domain.Core;

namespace Algorithms.Construction
{
    public interface IBuildRoutes
    {
        void Construct();
    }
    
    public class BuildRoutes : IBuildRoutes
    {
        private readonly IRouteRepository _routeRepository;

        public BuildRoutes(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }
        
        public void Construct()
        {
            throw new System.NotImplementedException();
        }
    }
}