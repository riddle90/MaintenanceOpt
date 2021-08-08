using System.Collections.Generic;
using Domain.Core.DistanceMatrixDomain;
using Domain.Core.LocationDomain;

namespace Infrastructure.Repository.DistanceMatrixRepositoryCollection
{
    public class DistanceMatrixRepository : IDistanceMatrixRepository
    {
        private readonly Dictionary<long, Dictionary<long, DistanceInfo>> _distanceMatrix;

        public DistanceMatrixRepository()
        {
            this._distanceMatrix = new Dictionary<long, Dictionary<long, DistanceInfo>>();
        }
        
        public void Add(long originCustomer, long destinationCustomer, DistanceInfo distanceInfo)
        {
            
            if(!this._distanceMatrix.ContainsKey(originCustomer))
                _distanceMatrix.Add(originCustomer, new Dictionary<long, DistanceInfo>());
            
            _distanceMatrix[originCustomer].Add(destinationCustomer, distanceInfo);

        }

        public float GetDistance(long originCustomer, long destinationCustomer)
        {
            return _distanceMatrix[originCustomer][destinationCustomer].Distance;
        }

        public float GetTime(long originCustomer, long destinationCustomer)
        {
            return _distanceMatrix[originCustomer][destinationCustomer].Time;
        }
    }
}