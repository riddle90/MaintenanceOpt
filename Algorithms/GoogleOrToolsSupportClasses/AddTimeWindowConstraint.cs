using System.Collections.Generic;
using Domain.Core;
using Google.OrTools.ConstraintSolver;

namespace Algorithms.GoogleOrToolsSupportClasses
{
    public class AddTimeWindowConstraint : IAddTimeWindowConstraint
    {
        public void AddConstraint(Stop stop, RoutingDimension timeDimension, int node,
            RoutingIndexManager routingIndexManager)
        {
            var index = routingIndexManager.NodeToIndex(node);
            timeDimension.CumulVar(index).SetRange(stop.TimeWindow.StartTime, stop.TimeWindow.EndTime);
        }

        public void AddConstraints(List<Stop> stops, RoutingDimension timeDimension,
            RoutingIndexManager routingIndexManager)
        {
            for (int i = 1; i <= stops.Count; i++)
            {
                this.AddConstraint(stops[i-1], timeDimension, i, routingIndexManager);
            }
        }

        public RoutingDimension AddTimeDimension(RoutingModel model, int timecallback)
        {
            model.AddDimension(timecallback, 30, 960, false, "Time");
            return model.GetMutableDimension("Time");
        }
    }
}