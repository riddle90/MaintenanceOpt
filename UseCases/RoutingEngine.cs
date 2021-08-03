using System;
using System.Threading.Tasks;
using UseCases.IAlgorithms;

namespace UseCases
{
    public class RoutingEngine : IRoutingEngine
    {
        private readonly IInputBuilder _inputBuilder;
        private readonly IOutputBuilder _outputBuilder;
        private readonly IOptimizer _optimizer;

        public RoutingEngine(IInputBuilder inputBuilder, IOutputBuilder outputBuilder, IOptimizer optimizer)
        {
            _inputBuilder = inputBuilder;
            _outputBuilder = outputBuilder;
            _optimizer = optimizer;
        }
        public async Task Run()
        {
            await this._inputBuilder.Build();
            this._optimizer.BuildRoutes();
            await this._outputBuilder.SaveResults();
        }
    }
}