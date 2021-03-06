using CsvHelper.Configuration;

namespace Infrastructure.DataAccess.Dtos
{
    public sealed class StopDataMapping : ClassMap<MaintenanceStopDto>
    {
        public StopDataMapping()
        {
            this.Map(x => x.CustomerID).Name("Customer ID");
            this.Map(x => x.Address).Name("Address");
            this.Map(x => x.ZIPCode).Name("ZIP Code");
            this.Map(x => x.City).Name("City");
        }
    }
}