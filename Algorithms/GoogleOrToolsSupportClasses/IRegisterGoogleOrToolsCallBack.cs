using System.Collections.Generic;
using Domain.Core;
using Google.OrTools.ConstraintSolver;

namespace Algorithms.GoogleOrToolsSupportClasses
{
    public interface IRegisterGoogleOrToolsCallBack
    {
        int RegisterDistance(RoutingModel model, List<Stop> stops, RoutingIndexManager manager);
        int RegisterTime(RoutingModel model, List<Stop> stops, RoutingIndexManager manager);
    }
}