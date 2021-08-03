﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.DataAccess.Dtos;

namespace Infrastructure.DataAccess
{
    public interface IStopDtoStore
    {
        Task<ICollection<MaintenanceStopDto>> GetMaintenanceStopDtos();
    }

    public interface IDistanceMatrixDtoStore
    {
        Task<ICollection<DistanceMatrixDto>> GetDistanceMatrixDtos(IEnumerable<MaintenanceStopDto> maintenanceStopDtos);
    }
}