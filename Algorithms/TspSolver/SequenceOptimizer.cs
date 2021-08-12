using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.FeasibilityAlgorithms;
using Algorithms.GoogleOrToolsSupportClasses;
using Domain.Core;

namespace Algorithms.TspSolver
{
    public class SequenceOptimizer : ISequenceOptimizer
    {
        private readonly IRunOptimization _runOptimization;
        private readonly IFeasibilityCheck _feasibilityCheck;

        public SequenceOptimizer(IRunOptimization runOptimization, IFeasibilityCheck feasibilityCheck)
        {
            _runOptimization = runOptimization;
            _feasibilityCheck = feasibilityCheck;
        }
        
        public Route Optimize(List<Stop> stops)
        {
            (List<Route> route, bool isRouteFeasible) = _runOptimization.Run(stops, 400, 0, 30);

            return route.Single();

        }
    }
}