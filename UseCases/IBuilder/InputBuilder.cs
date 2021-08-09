using System.Threading.Tasks;
using Infrastructure.DataAccess;
using Infrastructure.Repository.GoogleApis;
using Infrastructure.Repository.StopRepositoryCollection;
using Infrastructure.Repository.TerminalRepositoryCollection;

namespace UseCases
{
    public class InputBuilder : IInputBuilder
    {
        private readonly IStopDtoStore _stopDtoStore;
        private readonly IDistanceMatrixDtoStore _distanceMatrixDtoStore;
        private readonly IStopBuilder _stopBuilder;
        private readonly IDistanceMatrixBuilder _distanceMatrixBuilder;
        private readonly IGetDistanceMatrixApi _getDistanceMatrixApi;
        private readonly ITerminalBuilder _terminalBuilder;

        public InputBuilder(IStopDtoStore stopDtoStore, IDistanceMatrixDtoStore distanceMatrixDtoStore, IStopBuilder stopBuilder, 
            IDistanceMatrixBuilder distanceMatrixBuilder, IGetDistanceMatrixApi getDistanceMatrixApi, ITerminalBuilder terminalBuilder)
        {
            _stopDtoStore = stopDtoStore;
            _distanceMatrixDtoStore = distanceMatrixDtoStore;
            _stopBuilder = stopBuilder;
            _distanceMatrixBuilder = distanceMatrixBuilder;
            _getDistanceMatrixApi = getDistanceMatrixApi;
            _terminalBuilder = terminalBuilder;
        }
        
        public async Task Build(string apiKey)
        {
            var maintenanceStopDtos = await _stopDtoStore.GetMaintenanceStopDtos();
            _terminalBuilder.Build();
            _stopBuilder.BuildStops(maintenanceStopDtos);
            _getDistanceMatrixApi.GetMatrix(apiKey);

        }

        public async Task Build()
        {
            var maintenanceStopDtos = await _stopDtoStore.GetMaintenanceStopDtos();
            var distanceMatrixDtos = await _distanceMatrixDtoStore.GetDistanceMatrixDtos(maintenanceStopDtos);
            
            _terminalBuilder.Build();
            _stopBuilder.BuildStops(maintenanceStopDtos);
            _distanceMatrixBuilder.BuildDistanceMatrix(distanceMatrixDtos);

        }
    }
}