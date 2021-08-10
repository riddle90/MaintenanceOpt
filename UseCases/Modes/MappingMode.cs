using System.Threading.Tasks;

namespace UseCases
{
    public class MappingMode : IMappingMode
    {
        private readonly IInputBuilder _inputBuilder;
        private readonly IOutputBuilder _outputBuilder;

        public MappingMode(IInputBuilder inputBuilder, IOutputBuilder outputBuilder)
        {
            _inputBuilder = inputBuilder;
            _outputBuilder = outputBuilder;
        }
        public async Task Run(string apiKey)
        {
            await this._inputBuilder.Build(apiKey);
            await this._outputBuilder.SaveDistanceMatrix();
        }
    }
}