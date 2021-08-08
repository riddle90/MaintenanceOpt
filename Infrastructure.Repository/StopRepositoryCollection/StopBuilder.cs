using System;
using System.Collections.Generic;
using Domain.Core;
using Domain.Core.Common;
using Infrastructure.DataAccess;

namespace Infrastructure.Repository.StopRepositoryCollection
{
    public class StopBuilder : IStopBuilder
    {
        private readonly IStopRepository _stopRepository;

        public StopBuilder(IStopRepository stopRepository)
        {
            _stopRepository = stopRepository;
        }
        
        public void BuildStops(IEnumerable<MaintenanceStopDto> maintenanceStopDtos)
        {
            foreach (var maintenanceStopDto in maintenanceStopDtos)
            {
                var stop = new Stop(new Guid(), (long)maintenanceStopDto.CustomerID, maintenanceStopDto.Address,
                    maintenanceStopDto.ZIPCode, maintenanceStopDto.City, new TimeWindow(420, 960), 60);
                _stopRepository.AddStop(stop);
            }
        }
    }
}