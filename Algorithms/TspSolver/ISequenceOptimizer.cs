using System.Collections.Generic;
using Domain.Core;

namespace Algorithms.TspSolver
{
    public interface ISequenceOptimizer
    {
        Route Optimize(List<Stop> stops);
    }
}