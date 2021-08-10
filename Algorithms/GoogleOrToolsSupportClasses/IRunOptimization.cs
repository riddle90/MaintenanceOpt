using System.Collections.Generic;
using Domain.Core;

namespace Algorithms.GoogleOrToolsSupportClasses
{
    public interface IRunOptimization
    {
        (List<Route>, bool) Run(List<Stop> stops, int numVehicles, int depotId);
    }
}