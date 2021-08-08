namespace Infrastructure.Repository.GoogleApis
{
    public interface IGetDistanceMatrixApi
    {
        void GetMatrix(string apiKey);
    }
}