using System.Collections.Generic;
using System.Linq;
using Domain.Core;
using Domain.Core.DistanceMatrixDomain;
using Domain.Core.TerminalDomain;
using Infrastructure.DataAccess.Dtos;

namespace UseCases
{
    public class DistanceMatrixMapper
    {
        private readonly IDistanceMatrixRepository _distanceMatrixRepository;
        private readonly IStopRepository _stopRepository;
        private readonly ITerminalRepository _terminalRepository;


        public DistanceMatrixMapper(IDistanceMatrixRepository distanceMatrixRepository, IStopRepository stopRepository,
            ITerminalRepository terminalRepository)
        {
            _distanceMatrixRepository = distanceMatrixRepository;
            _stopRepository = stopRepository;
            _terminalRepository = terminalRepository;
        }

        public ICollection<DistanceMatrixDto> GetDistanceMatrixDtoStore()
        {
            var distanceMatrixDtos = new List<DistanceMatrixDto>();
            var customerIds = _stopRepository.GetStops().Select(x => x.CustomerId).ToList();
            if(!_terminalRepository.GetTerminal().IsDummy)
                customerIds.Add(_terminalRepository.GetTerminal().CustomerId);
            
            foreach (var originCustomerId in customerIds)
            {
                foreach (var destCustomerId in customerIds)
                {
                    var distance = _distanceMatrixRepository.GetDistance(originCustomerId, destCustomerId);
                    var time = _distanceMatrixRepository.GetTime(originCustomerId, destCustomerId);
                    distanceMatrixDtos.Add(new DistanceMatrixDto
                    {
                        OriginCustomerId = (int)originCustomerId,
                        DestinationCustomerId = (int)destCustomerId,
                        Distance = distance,
                        Time = time,
                    });

                }
            }

            return distanceMatrixDtos;

        }
    }
}