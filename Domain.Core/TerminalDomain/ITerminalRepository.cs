namespace Domain.Core.TerminalDomain
{
    public interface ITerminalRepository
    {
        Terminal GetTerminal();
        void AddTerminal(Terminal terminal);
    }
}