using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using Domain.Core;
using Infrastructure.DataAccess.Dtos;
using Infrastructure.Repository.TerminalRepositoryCollection;

namespace UseCases
{
    public class RouteMapper 
    {
        private readonly IRouteRepository _routeRepository;

        public RouteMapper(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public ICollection<RouteStopDto> CreateRouteStopDtos()
        {
            var routeStopDtos = new List<RouteStopDto>();
            foreach (var route in _routeRepository.GetAllRoutes())
            {
                foreach (var stop in route.GetStops())
                {
                    int stopSequence = 0;
                    var routeStopDto = new RouteStopDto
                    {
                        RouteID = route.RouteId, StopSequence = ++stopSequence, CustomerId = stop.CustomerId,
                        ETA = TimeSpan.FromMinutes(route.GetStopArrivalTime(stop)),
                        ETD = TimeSpan.FromMinutes(route.GetStopArrivalTime(stop) + 60)
                    };
                    
                    routeStopDtos.Add(routeStopDto);
                }
            }

            return routeStopDtos;
        }
    }
}