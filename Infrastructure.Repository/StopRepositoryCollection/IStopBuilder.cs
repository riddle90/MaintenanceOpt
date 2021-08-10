using System.Collections.Generic;
using Infrastructure.DataAccess;

namespace Infrastructure.Repository.StopRepositoryCollection
{
    public interface IStopBuilder
    {
        void BuildStops(IEnumerable<MaintenanceStopDto> maintenanceStopDtos);
    }
}