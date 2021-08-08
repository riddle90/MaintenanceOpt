using System.Collections.Generic;
using Domain.Core;

namespace Algorithms.FeasibilityAlgorithms
{
    public interface IFeasibilityCheck
    {
        RouteDetails CheckFeasibility(List<Stop> stops);
    }
}