using System.Collections.Generic;
using Domain.Core;

namespace Algorithms.TspSolver
{
    public interface IRunOptimization
    {
        List<Stop> Run(List<Stop> stops);
    }
}