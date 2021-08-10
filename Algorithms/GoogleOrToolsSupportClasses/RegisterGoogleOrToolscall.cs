using System.Collections.Generic;
using Domain.Core;
using Domain.Core.DistanceMatrixDomain;
using Google.OrTools.ConstraintSolver;

namespace Algorithms.GoogleOrToolsSupportClasses
{
    public class RegisterGoogleOrToolsCall : IRegisterGoogleOrToolsCallBack
    {
        private readonly IDistanceMatrixRepository _distanceMatrixRepository;

        public RegisterGoogleOrToolsCall(IDistanceMatrixRepository distanceMatrixRepository)
        {
            _distanceMatrixRepository = distanceMatrixRepository;
        }
        
        public int RegisterDistance(RoutingModel model, List<Stop> stops, RoutingIndexManager manager)
        {
            return model.RegisterTransitCallback(((fromIndex, toIndex) =>
            {
                var fromNode = manager.IndexToNode(fromIndex);
                var toNode = manager.IndexToNode(toIndex);    
                var originCustomer = fromNode == 0 ? 0 : stops[(int)(fromNode - 1)].CustomerId;   
                var destinationCustomer = toNode == 0 ? 0 : stops[(int)(toNode - 1)].CustomerId;
                return (int) this._distanceMatrixRepository.GetDistance(originCustomer, destinationCustomer);
            }));
        }

        public int RegisterTime(RoutingModel model, List<Stop> stops, RoutingIndexManager manager)
        {
            return model.RegisterTransitCallback(((fromIndex, toIndex) =>
            {
                var fromNode = manager.IndexToNode(fromIndex);
                var toNode = manager.IndexToNode(toIndex);    
                var originCustomer = fromNode == 0 ? 0 : stops[(int)(fromNode - 1)].CustomerId;   
                var destinationCustomer = toNode == 0 ? 0 : stops[(int)(toNode - 1)].CustomerId;

                if (originCustomer == 0 || destinationCustomer == 0)
                {
                    return (int) this._distanceMatrixRepository.GetTime(originCustomer, destinationCustomer);
                }
                    
                return (int) (this._distanceMatrixRepository.GetTime(originCustomer, destinationCustomer) + stops[(int)(fromNode - 1)].StopTime);
            }));
        }
    }
}