using System.Collections.Generic;
using Domain.Core;
using Google.OrTools.ConstraintSolver;

namespace Algorithms.GoogleOrToolsSupportClasses
{
    public interface IAddTimeWindowConstraint
    {
        void AddConstraint(Stop stop, RoutingDimension timeDimension, int index, RoutingIndexManager routingIndexManager);

        void AddConstraints(List<Stop> stops, RoutingDimension timeDimension, RoutingIndexManager routingIndexManager);
        
        RoutingDimension AddTimeDimension(RoutingModel model, int timecallback);
    }
}