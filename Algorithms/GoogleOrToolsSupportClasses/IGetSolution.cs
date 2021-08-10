using System.Collections.Generic;
using Domain.Core;
using Google.OrTools.ConstraintSolver;

namespace Algorithms.GoogleOrToolsSupportClasses
{
    public interface IGetSolution
    {
        (List<Route>, bool) GetSolution(RoutingModel model, Assignment solution, List<Stop> stops, int numVehicles,
            int depotId, RoutingIndexManager routingIndexManager, RoutingDimension routingDimension);
       
    }
}