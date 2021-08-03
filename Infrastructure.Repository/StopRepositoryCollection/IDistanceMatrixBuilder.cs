using System.Collections.Generic;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Dtos;

namespace Infrastructure.Repository.StopRepositoryCollection
{
    public interface IDistanceMatrixBuilder
    {
        void BuildDistanceMatrix(IEnumerable<DistanceMatrixDto> distanceMatrixDtos);
    }
}