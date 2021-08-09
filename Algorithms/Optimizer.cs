using System;
using Algorithms.Construction;
using UseCases.IAlgorithms;

namespace Algorithms
{
    public class Optimizer : IOptimizer
    {
        private readonly IBuildRoutes _buildRoutes;

        public Optimizer(IBuildRoutes buildRoutes)
        {
            _buildRoutes = buildRoutes;
        }
        
        public void Optimize()
        {
            _buildRoutes.Construct();
        }
    }
}