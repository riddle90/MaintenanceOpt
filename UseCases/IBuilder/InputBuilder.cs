using System.Threading.Tasks;
using Infrastructure.DataAccess;
using Infrastructure.Repository.StopRepositoryCollection;

namespace UseCases
{
    public class InputBuilder : IInputBuilder
    {
        private readonly IStopDtoStore _stopDtoStore;
        private readonly IDistanceMatrixDtoStore _distanceMatrixDtoStore;
        private readonly IStopBuilder _stopBuilder;
        private readonly IDistanceMatrixBuilder _distanceMatrixBuilder;

        public InputBuilder(IStopDtoStore stopDtoStore, IDistanceMatrixDtoStore distanceMatrixDtoStore, IStopBuilder stopBuilder, IDistanceMatrixBuilder distanceMatrixBuilder)
        {
            _stopDtoStore = stopDtoStore;
            _distanceMatrixDtoStore = distanceMatrixDtoStore;
            _stopBuilder = stopBuilder;
            _distanceMatrixBuilder = distanceMatrixBuilder;
        }
        
        public async Task Build()
        {
            var maintenanceStopDtos = await _stopDtoStore.GetMaintenanceStopDtos();
            var distanceMatrixDtos = await _distanceMatrixDtoStore.GetDistanceMatrixDtos(maintenanceStopDtos);
            _stopBuilder.BuildStops(maintenanceStopDtos);
            _distanceMatrixBuilder.BuildDistanceMatrix(distanceMatrixDtos);

        }
    }
}