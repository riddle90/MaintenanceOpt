using System.Collections.Generic;
using Domain.Core;
using Domain.Core.DistanceMatrixDomain;
using Google.OrTools.ConstraintSolver;
using Microsoft.Extensions.Logging;
using Google.Protobuf.WellKnownTypes;

namespace Algorithms.GoogleOrToolsSupportClasses
{
    public class RunGoogleOrOptimization : IRunOptimization
    {
        private readonly IDistanceMatrixRepository _distanceMatrixRepository;
        private readonly IRegisterGoogleOrToolsCallBack _callbacks;
        private readonly IGetSolution _solution;
        private readonly IAddTimeWindowConstraint _timeWindowConstraint;
        private readonly ILogger _logger;


        public RunGoogleOrOptimization(IDistanceMatrixRepository distanceMatrixRepository, ILoggerFactory loggerFactory, 
            IRegisterGoogleOrToolsCallBack callbacks, IGetSolution solution, IAddTimeWindowConstraint timeWindowConstraint)
        {
            _distanceMatrixRepository = distanceMatrixRepository;
            _callbacks = callbacks;
            _solution = solution;
            _timeWindowConstraint = timeWindowConstraint;
            _logger = loggerFactory.CreateLogger(this.GetType().Name);
        }
        public (List<Route>, bool) Run(List<Stop> stops, int numVehicles, int depotId, int duration)
        {
            RoutingIndexManager manager = new RoutingIndexManager(stops.Count + 1, numVehicles, depotId);
            RoutingModel model = new RoutingModel(manager);
            var distance = _callbacks.RegisterDistance(model, stops, manager);
            var time = _callbacks.RegisterTime(model, stops, manager);

            var timedimension = _timeWindowConstraint.AddTimeDimension(model, time);
            _timeWindowConstraint.AddConstraints(stops, timedimension, manager);
            
            model.SetArcCostEvaluatorOfAllVehicles(distance);
            
            
            var searchParameters = operations_research_constraint_solver.DefaultRoutingSearchParameters();
            searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;
            searchParameters.LocalSearchMetaheuristic = LocalSearchMetaheuristic.Types.Value.GuidedLocalSearch;
            searchParameters.LogSearch = true;
            searchParameters.TimeLimit = new Duration {Seconds = duration};

            var solution = model.SolveWithParameters(searchParameters);

            if (solution == null)
                return (new List<Route>(), false);

            return _solution.GetSolution(model, solution, stops, numVehicles, depotId, manager, timedimension);

        }
    }
}