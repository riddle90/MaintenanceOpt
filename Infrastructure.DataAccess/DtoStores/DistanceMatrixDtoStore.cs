using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Infrastructure.DataAccess.Dtos;

namespace Infrastructure.DataAccess
{
    public class DistanceMatrixDtoStore : IDistanceMatrixDtoStore
    {
        public async Task<ICollection<DistanceMatrixDto>> GetDistanceMatrixDtos(
            IEnumerable<MaintenanceStopDto> maintenanceStopDtos)
        {
            using (TextReader reader = await FileReader.GetReader("DistanceMatrix.csv"))
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true,
                };
                
                configuration.IgnoreBlankLines = true;
                using (var csv = new CsvReader(reader, configuration))
                {
                    csv.Context.RegisterClassMap<DistanceDataMapping>();
                    return csv.GetRecords<DistanceMatrixDto>().ToList();
                }
            }
        }
        
        
    }
}