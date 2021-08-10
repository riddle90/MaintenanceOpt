using System;
using System.Collections.Generic;
using Algorithms.FeasibilityAlgorithms;
using Domain.Core;
using Google.OrTools.ConstraintSolver;
using Microsoft.Extensions.Logging;

namespace Algorithms.GoogleOrToolsSupportClasses
{
    public class GetGoogleOrToolsSolution : IGetSolution
    {
        private readonly IFeasibilityCheck _feasibilityCheck;
        private readonly IRouteRepository _routeRepository;
        private readonly ILogger _logger;

        public GetGoogleOrToolsSolution(IFeasibilityCheck feasibilityCheck, IRouteRepository routeRepository,
            ILoggerFactory loggerFactory)
        {
            _feasibilityCheck = feasibilityCheck;
            _routeRepository = routeRepository;
            _logger = loggerFactory.CreateLogger(this.GetType().Name);
        }
        public (List<Route>, bool) GetSolution(RoutingModel model, Assignment solution, List<Stop> stops,
            int numVehicles,
            int depotId, RoutingIndexManager routingIndexManager, RoutingDimension timeDimension)
        {
            var routeList = new List<Route>();
            bool allRoutesFeasible = true;
            for (int i = 0; i < numVehicles; i++)
            {
                var stopList = new List<Stop>();
                var routeStartTime = 0;
                var index = model.Start(i);
                while (model.IsEnd(index) == false)
                {
                    var node = routingIndexManager.IndexToNode(index);
                    if (node != 0)
                    {
                        stopList.Add(stops[(int) (node - 1)]);
                        var arrivalTime = (int)solution.Min(timeDimension.CumulVar(index));
                        if (arrivalTime > stops[(node - 1)].TimeWindow.EndTime)
                        {
                            
                        }
                    }
                    else
                    {
                        routeStartTime = (int)solution.Max(timeDimension.CumulVar(index));
                    }
                    
           
                    index = solution.Value(model.NextVar(index));
                }

                var routeDetails = _feasibilityCheck.CheckFeasibility(stopList, routeStartTime);
                allRoutesFeasible = allRoutesFeasible & routeDetails.Status == FeasibilityStatus.Feasible;
                var route = new Route(Guid.NewGuid(), stopList, routeDetails);
                
                routeList.Add(route);

               
            }

            string routeStatus = allRoutesFeasible ? "Feasible" : "Not Feasible";
            _logger.LogDebug($"Routes added to Route repo : and all routes are {routeStatus}");
            return (routeList, allRoutesFeasible);

        }
    }
}