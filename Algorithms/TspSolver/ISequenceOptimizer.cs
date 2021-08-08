using System;
using System.Collections.Generic;
using Algorithms.FeasibilityAlgorithms;
using Domain.Core;

namespace Algorithms.TspSolver
{
    public interface ISequenceOptimizer
    {
        Route Optimize(List<Stop> stops);
    }

    public interface IRunOptimization
    {
        List<Stop> Run(List<Stop> stops);
    }
    
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
            var optimizedStops = _runOptimization.Run(stops);
            var routeDetails = _feasibilityCheck.CheckFeasibility(optimizedStops);
            if (routeDetails.Status != FeasibilityStatus.Feasible)
            {
                return null;
            }

            return new Route(new Guid(), optimizedStops, routeDetails);

        }
    }
}