using System.Collections.Generic;
using Domain.Core.DistanceMatrixDomain;
using Domain.Core.LocationDomain;

namespace Infrastructure.Repository.DistanceMatrixRepositoryCollection
{
    public class DistanceMatrixRepository : IDistanceMatrixRepository
    {
        private readonly Dictionary<int, Dictionary<int, DistanceInfo>> _distanceMatrix;

        public DistanceMatrixRepository()
        {
            this._distanceMatrix = new Dictionary<int, Dictionary<int, DistanceInfo>>();
        }
        
        public void Add(int originCustomer, int destinationCustomer, DistanceInfo distanceInfo)
        {
            
            if(!this._distanceMatrix.ContainsKey(originCustomer))
                _distanceMatrix.Add(originCustomer, new Dictionary<int, DistanceInfo>());
            
            _distanceMatrix[originCustomer].Add(destinationCustomer, distanceInfo);

        }

        public float GetDistance(int originCustomer, int destinationCustomer)
        {
            return _distanceMatrix[originCustomer][destinationCustomer].Distance;
        }

        public float GetTime(int originCustomer, int destinationCustomer)
        {
            return _distanceMatrix[originCustomer][destinationCustomer].Time;
        }
    }
}