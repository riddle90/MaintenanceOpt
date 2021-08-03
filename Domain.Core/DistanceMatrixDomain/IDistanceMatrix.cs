using Domain.Core.LocationDomain;

namespace Domain.Core.DistanceMatrixDomain
{
    public interface IDistanceMatrix
    {
        void Add(int originCustomer, int destinationCustomer, DistanceInfo distanceInfo);

        float GetDistance(int originCustomer, int destinationCustomer);

        float GetTime(int originCustomer, int destinationCustomer);
    }
}