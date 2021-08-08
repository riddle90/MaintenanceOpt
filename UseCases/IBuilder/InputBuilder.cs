using System.Threading.Tasks;
using Infrastructure.DataAccess;
using Infrastructure.Repository.GoogleApis;
using Infrastructure.Repository.StopRepositoryCollection;

namespace UseCases
{
    public class InputBuilder : IInputBuilder
    {
        private readonly IStopDtoStore _stopDtoStore;
        private readonly IDistanceMatrixDtoStore _distanceMatrixDtoStore;
        private readonly IStopBuilder _stopBuilder;
        private readonly IDistanceMatrixBuilder _distanceMatrixBuilder;
        private readonly IGetDistanceMatrixApi _getDistanceMatrixApi;

        public InputBuilder(IStopDtoStore stopDtoStore, IDistanceMatrixDtoStore distanceMatrixDtoStore, IStopBuilder stopBuilder, 
            IDistanceMatrixBuilder distanceMatrixBuilder, IGetDistanceMatrixApi getDistanceMatrixApi)
        {
            _stopDtoStore = stopDtoStore;
            _distanceMatrixDtoStore = distanceMatrixDtoStore;
            _stopBuilder = stopBuilder;
            _distanceMatrixBuilder = distanceMatrixBuilder;
            _getDistanceMatrixApi = getDistanceMatrixApi;
        }
        
        public async Task Build(string apiKey)
        {
            var maintenanceStopDtos = await _stopDtoStore.GetMaintenanceStopDtos();
            //var distanceMatrixDtos = await _distanceMatrixDtoStore.GetDistanceMatrixDtos(maintenanceStopDtos);
            _stopBuilder.BuildStops(maintenanceStopDtos);
            _getDistanceMatrixApi.GetMatrix(apiKey);
            //_distanceMatrixBuilder.BuildDistanceMatrix(distanceMatrixDtos);

        }
    }
}