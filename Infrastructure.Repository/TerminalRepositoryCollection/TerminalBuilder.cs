using Domain.Core.TerminalDomain;

namespace Infrastructure.Repository.TerminalRepositoryCollection
{
    public class TerminalBuilder : ITerminalBuilder
    {
        private readonly ITerminalRepository _terminalRepository;

        public TerminalBuilder(ITerminalRepository terminalRepository)
        {
            _terminalRepository = terminalRepository;
        }
        
        public void Build()
        {
            var terminal = new Terminal(0, null, null, null, true);
            _terminalRepository.AddTerminal(terminal);
        }
    }
}