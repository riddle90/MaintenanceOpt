using System.Collections.Generic;
using Domain.Core;
using Domain.Core.DistanceMatrixDomain;
using Google.OrTools.ConstraintSolver;
using Microsoft.Extensions.Logging;

namespace Algorithms.TspSolver
{
    public class RunOptimization : IRunOptimization
    {
        private readonly IDistanceMatrixRepository _distanceMatrixRepository;
        private readonly ILogger _logger;


        public RunOptimization(IDistanceMatrixRepository distanceMatrixRepository, ILoggerFactory loggerFactory)
        {
            _distanceMatrixRepository = distanceMatrixRepository;
            _logger = loggerFactory.CreateLogger(this.GetType().Name);
        }
        public List<Stop> Run(List<Stop> stops)
        {
            RoutingIndexManager manager = new RoutingIndexManager(stops.Count, 1, 0);
            RoutingModel model = new RoutingModel(manager);
            var distance =  model.RegisterTransitCallback(((FromIndex, ToIndex) =>
            {
                var originCustomer = FromIndex == 0 ? 0 : stops[(int)(FromIndex - 1)].CustomerId;   
                var destinationCustomer = ToIndex == 0 ? 0 : stops[(int)(ToIndex - 1)].CustomerId;
                return (int) this._distanceMatrixRepository.GetDistance(originCustomer, destinationCustomer);
            }));
            model.SetArcCostEvaluatorOfAllVehicles(distance);
            var searchParameters = operations_research_constraint_solver.DefaultRoutingSearchParameters();
            searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

            var solution = model.SolveWithParameters(searchParameters);

            return GetSolution(manager, model, solution, stops);

        }

        private List<Stop> GetSolution(RoutingIndexManager manager, RoutingModel model, Assignment solution,
            List<Stop> stops)
        {
            var newOrder = new List<Stop>();
            
            var index = model.Start(0);
            while (model.IsEnd(index) == false)
            {
                var previousIndex = index;
                index = solution.Value(model.NextVar(index));
                _logger.LogDebug("{0} -> ", manager.IndexToNode((int)index));
                newOrder.Add(stops[(int)(index - 1)]);
            }

            return newOrder;
        }

    }
}