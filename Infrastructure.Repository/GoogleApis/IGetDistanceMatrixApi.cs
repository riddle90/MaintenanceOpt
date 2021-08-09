using System.Threading.Tasks;

namespace Infrastructure.Repository.GoogleApis
{
    public interface IGetDistanceMatrixApi
    {
        Task GetMatrix(string apiKey);
    }
}