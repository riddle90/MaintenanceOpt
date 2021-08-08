using System.Threading.Tasks;

namespace UseCases
{
    public interface IInputBuilder
    {
        Task Build(string apiKey);
    }
}