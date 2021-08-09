using System.Threading.Tasks;

namespace UseCases
{
    public interface IMappingMode
    {
        Task Run(string apiKey);
    }
}