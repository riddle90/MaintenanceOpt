using Domain.Core.TerminalDomain;

namespace Infrastructure.Repository.TerminalRepositoryCollection
{
    public class TerminalRepository : ITerminalRepository
    {
        private Terminal _terminal;
        
        public Terminal GetTerminal()
        {
            return _terminal;
        }

        public void AddTerminal(Terminal terminal)
        {
            _terminal = terminal;
        }
    }
}