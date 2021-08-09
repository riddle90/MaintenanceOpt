using System.Linq;
using Algorithms.TspSolver;
using Domain.Core;

namespace Algorithms.Construction
{
    public class BuildRoutes : IBuildRoutes
    {
        private readonly IRouteRepository _routeRepository;
        private readonly ISequenceOptimizer _sequenceOptimizer;
        private readonly IStopRepository _stopRepository;

        public BuildRoutes(IRouteRepository routeRepository, ISequenceOptimizer sequenceOptimizer, IStopRepository stopRepository)
        {
            _routeRepository = routeRepository;
            _sequenceOptimizer = sequenceOptimizer;
            _stopRepository = stopRepository;
        }
        
        public void Construct()
        {
            var route = _sequenceOptimizer.Optimize(_stopRepository.GetStops().ToList());
            _routeRepository.AddRoute(route);
        }
    }
}