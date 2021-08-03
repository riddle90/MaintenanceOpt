using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public interface IStopDtoStore
    {
        Task<ICollection<MaintenanceStopDto>> GetMaintenanceStopDtos();
    }
}