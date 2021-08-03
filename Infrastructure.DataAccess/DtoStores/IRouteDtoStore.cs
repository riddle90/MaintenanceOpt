using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.DataAccess.Dtos;

namespace Infrastructure.DataAccess
{
    public interface IRouteDtoStore
    {
        Task WriteSolution(IEnumerable<RouteStopDto> routeStopDtos);
    }
}