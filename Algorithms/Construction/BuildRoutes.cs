using System.Collections.Generic;
using System.Linq;
using Algorithms.GoogleOrToolsSupportClasses;
using Algorithms.TspSolver;
using Domain.Core;
using Microsoft.Extensions.Logging;

namespace Algorithms.Construction
{
    public class BuildRoutes : IBuildRoutes
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IRunOptimization _runOptimization;
        private readonly IStopRepository _stopRepository;
        private readonly ILogger _logger;

        public BuildRoutes(IRouteRepository routeRepository, IRunOptimization runOptimization,
            IStopRepository stopRepository, ILoggerFactory loggerFactory)
        {
            _routeRepository = routeRepository;
            _runOptimization = runOptimization;
            _stopRepository = stopRepository;
            _logger = loggerFactory.CreateLogger(this.GetType().Name);
        }
        
        public void Construct()
        {
            bool stillSearching = true;
            int smallestFeasible = 2770;
            int largestInfeasible = 0;
            int numVehicles = 600;
            
            while(stillSearching)
            {
                (var routes, var allRoutesFeasible) = _runOptimization.Run(_stopRepository.GetStops().ToList(), numVehicles, 0, 15);

                if (allRoutesFeasible)
                {
                    smallestFeasible = numVehicles;
                    _logger.LogDebug($"Found Feasible Solution with {numVehicles}");
                }
                else
                {
                    largestInfeasible = numVehicles;
                    _logger.LogDebug($"Solution Infeasible with {numVehicles}");

                }
                numVehicles = (largestInfeasible + smallestFeasible) / 2;

                if (smallestFeasible - numVehicles < 5)
                {
                    stillSearching = false;
                }
            }

            stillSearching = true;
            while (stillSearching)
            {
                numVehicles = smallestFeasible - 1;
                (var routes, var allRoutesFeasible) = _runOptimization.Run(_stopRepository.GetStops().ToList(), numVehicles, 0, 30);

                if (allRoutesFeasible)
                {
                    _logger.LogDebug($"Found Feasible Solution with {numVehicles}");
                    smallestFeasible = numVehicles;
                }
                else
                {
                    largestInfeasible = numVehicles;
                    stillSearching = false;
                    _logger.LogDebug($"Solution Infeasible with {numVehicles}");

                }
            }
            
            (var bestKnownSolution, var _) = _runOptimization.Run(_stopRepository.GetStops().ToList(), smallestFeasible, 0, 300);

            _routeRepository.AddRoutes(bestKnownSolution);
        }
    }
}