using System.Collections.Generic;
using Domain.Core.DistanceMatrixDomain;
using Domain.Core.LocationDomain;
using Infrastructure.DataAccess.Dtos;

namespace Infrastructure.Repository.StopRepositoryCollection
{
    public class DistanceMatrixBuilder : IDistanceMatrixBuilder
    {
        private readonly IDistanceMatrixRepository _distanceMatrix;

        public DistanceMatrixBuilder(IDistanceMatrixRepository distanceMatrix)
        {
            _distanceMatrix = distanceMatrix;
        }
        
        public void BuildDistanceMatrix(IEnumerable<DistanceMatrixDto> distanceMatrixDtos)
        {
            foreach (var distanceMatrixDto in distanceMatrixDtos)
            {
                var distanceInfo = new DistanceInfo(distanceMatrixDto.Distance, distanceMatrixDto.Time);
                _distanceMatrix.Add(distanceMatrixDto.OriginCustomerId, distanceMatrixDto.DestinationCustomerId, distanceInfo);
            }
        }
    }
}