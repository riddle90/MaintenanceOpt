using System;
using System.Threading.Tasks;
using Infrastructure.DataAccess;

namespace UseCases
{
    public class OutputBuilder : IOutputBuilder
    {
        private readonly RouteMapper _routeMapper;
        private readonly IRouteDtoStore _routeDtoStore;
        private readonly IDistanceMatrixDtoStore _distanceMatrixDtoStore;
        private readonly DistanceMatrixMapper _distanceMatrixMapper;


        public OutputBuilder(RouteMapper routeMapper, IRouteDtoStore routeDtoStore,
            IDistanceMatrixDtoStore distanceMatrixDtoStore, DistanceMatrixMapper distanceMatrixMapper)
        {
            _routeMapper = routeMapper;
            _routeDtoStore = routeDtoStore;
            _distanceMatrixDtoStore = distanceMatrixDtoStore;
            _distanceMatrixMapper = distanceMatrixMapper;
        }
        public async Task SaveRoutingResults()
        {
            var routeStopDtos = _routeMapper.CreateRouteStopDtos();
            await this._routeDtoStore.WriteSolution(routeStopDtos);
        }

        public async Task SaveDistanceMatrix()
        {
            var distanceMatrixDtos = _distanceMatrixMapper.GetDistanceMatrixDtoStore();
            await this._distanceMatrixDtoStore.WriteDistanceMatrixDtos(distanceMatrixDtos);
        }
    }
}