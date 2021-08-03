using System;
using System.Threading.Tasks;
using Infrastructure.DataAccess;

namespace UseCases
{
    public class OutputBuilder : IOutputBuilder
    {
        private readonly RouteMapper _routeMapper;
        private readonly IRouteDtoStore _routeDtoStore;


        public OutputBuilder(RouteMapper routeMapper, IRouteDtoStore routeDtoStore)
        {
            _routeMapper = routeMapper;
            _routeDtoStore = routeDtoStore;
        }
        public async Task SaveResults()
        {
            var routeStopDtos = _routeMapper.CreateRouteStopDtos();
            await this._routeDtoStore.WriteSolution(routeStopDtos);
        }
    }
}