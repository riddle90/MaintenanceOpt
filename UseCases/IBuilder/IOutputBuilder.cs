using System.Threading.Tasks;
using Domain.Core;

namespace UseCases
{
    public interface IOutputBuilder
    {
        Task SaveResults();
    }
}