using Domain.Core.LocationDomain;

namespace Domain.Core.DistanceMatrixDomain
{
    public interface IDistanceMatrixRepository
    {
        void Add(long originCustomer, long destinationCustomer, DistanceInfo distanceInfo);

        float GetDistance(long originCustomer, long destinationCustomer);

        float GetTime(long originCustomer, long destinationCustomer);
    }
}