using Algorithms;
using Algorithms.Construction;
using Algorithms.FeasibilityAlgorithms;
using Algorithms.TspSolver;
using Domain.Core;
using Domain.Core.DistanceMatrixDomain;
using Domain.Core.TerminalDomain;
using Infrastructure.DataAccess;
using Infrastructure.Repository;
using Infrastructure.Repository.DistanceMatrixRepositoryCollection;
using Infrastructure.Repository.GoogleApis;
using Infrastructure.Repository.StopRepositoryCollection;
using Infrastructure.Repository.TerminalRepositoryCollection;
using SimpleInjector;
using UseCases;
using UseCases.IAlgorithms;

namespace RoutingEngine.Webapp
{
    public static class RoutingEngineBootStrapper
    {
        public static void InitializeContainer(Container container)
        {
            InitializeRepository(container);
            InitializeAlgorithms(container);
            InitializeUsecases(container);
            InitializeDtoStores(container);
            
        }

        private static void InitializeRepository(Container container)
        {
            container.Register<IRouteRepository, RouteRepository>();
            container.Register<IStopRepository, StopRepository>();
            container.Register<IDistanceMatrixRepository, DistanceMatrixRepository>();
            container.Register<IGetDistanceMatrixApi, GetGoogleDistanceMatrixApi>();
        }

        private static void InitializeAlgorithms(Container container)
        {
            container.Register<IOptimizer, Optimizer>();
            container.Register<IBuildRoutes, BuildRoutes>();
            container.Register<ISequenceOptimizer, SequenceOptimizer>();
            container.Register<IRunOptimization, RunOptimization>();
            container.Register<IFeasibilityCheck, FeasibilityCheck>();
        }

        private static void InitializeUsecases(Container container)
        {
            container.Register<IStopBuilder, StopBuilder>();
            container.Register<IDistanceMatrixBuilder, DistanceMatrixBuilder>();
            container.Register<IInputBuilder, InputBuilder>();
            container.Register<IOutputBuilder, OutputBuilder>();
            container.Register<IRoutingEngine, UseCases.RoutingEngine>();
            container.Register<IMappingMode, MappingMode>();
            container.Register<ITerminalBuilder, TerminalBuilder>();
            
        }

        private static void InitializeDtoStores(Container container)
        {
            container.Register<IStopDtoStore, StopDtoStore>();
            container.Register<IDistanceMatrixDtoStore, DistanceMatrixDtoStore>();
            container.Register<IRouteDtoStore, RouteDtoStore>();
            container.Register<ITerminalRepository, TerminalRepository>();
        }
    }
}