using System.Threading.Tasks;

namespace UseCases
{
    public interface IRoutingEngine
    {
        Task Run(string apiKey);
    }
}