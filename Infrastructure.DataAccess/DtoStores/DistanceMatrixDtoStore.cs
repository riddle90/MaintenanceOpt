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
        private readonly string _filename;
        private readonly string _folder;

        public DistanceMatrixDtoStore()
        {
            _filename = "DistanceMatrix_full.csv";
            _folder = "Data";
        }
        
        public async Task<ICollection<DistanceMatrixDto>> GetDistanceMatrixDtos(
            IEnumerable<MaintenanceStopDto> maintenanceStopDtos)
        {
            //var path = Path.Combine(_folder, _filename);

            using (TextReader reader = await FileReader.GetReader(_filename))
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
        
        public async Task WriteDistanceMatrixDtos(IEnumerable<DistanceMatrixDto> distanceMatrixDtos)
        {
            
            var path = Path.Combine(_folder, _filename);
            using (FileStream stream = File.Create(path))
            {
                TextWriter writer = new StreamWriter(stream);
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = false,
                };

                using (var csv = new CsvWriter(writer, configuration))
                {
                    csv.Context.RegisterClassMap<DistanceDataMapping>();
                    csv.WriteHeader<DistanceMatrixDto>();
                    csv.NextRecord();
                    await csv.WriteRecordsAsync(distanceMatrixDtos);
                }
            }
        }
        
        
    }
}