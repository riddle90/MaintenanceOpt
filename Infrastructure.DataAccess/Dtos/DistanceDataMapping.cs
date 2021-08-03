using CsvHelper.Configuration;

namespace Infrastructure.DataAccess.Dtos
{
    public sealed class DistanceDataMapping : ClassMap<DistanceMatrixDto>
    {
        public DistanceDataMapping()
        {
            this.Map(x => x.OriginCustomerId).Name("Origin Customer ID");
            this.Map(x => x.DestinationCustomerId).Name("Destination Customer ID");
            this.Map(x => x.Distance).Name("Distance");
            this.Map(x => x.Time).Name("Time");
        }
    }
}