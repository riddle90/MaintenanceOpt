using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Infrastructure.DataAccess.Dtos;

namespace Infrastructure.DataAccess
{
    public class RouteDtoStore : IRouteDtoStore
    {
        public async Task WriteSolution(IEnumerable<RouteStopDto> routeStopDtos)
        {
            var folderName = "Data";
            var fileName = "solution.csv";
            var path = Path.Combine(folderName, fileName);
            using (FileStream stream = File.Create(path))
            {
                TextWriter writer = new StreamWriter(stream);
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ","
                };

                using (var csv = new CsvWriter(writer, configuration))
                {
                    csv.WriteHeader(typeof(RouteStopDto));
                    await csv.WriteRecordsAsync(routeStopDtos);
                }
            }
        }
    }
}