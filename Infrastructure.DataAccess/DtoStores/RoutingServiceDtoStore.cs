using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Infrastructure.DataAccess.Dtos;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataAccess
{
    public interface IRoutingServiceDtoStore
    {
        Task<ICollection<MaintenanceStopDto>> GetMaintenanceStopDtos();
    }

    public class FileRoutingServiceDtoStore : IRoutingServiceDtoStore
    {
        private readonly IConfiguration _configuration;

        public FileRoutingServiceDtoStore(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<ICollection<MaintenanceStopDto>> GetMaintenanceStopDtos()
        {
            using (TextReader reader = await this.GetReader())
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true,
                };
                
                configuration.IgnoreBlankLines = true;
                using (var csv = new CsvReader(reader, configuration))
                {
                    csv.Context.RegisterClassMap<DataMapping>();
                    return csv.GetRecords<MaintenanceStopDto>().ToList();
                }
            }
        }

        private async Task<TextReader> GetReader()
        {
            try
            {
                var folder = "Data";
                var filename = $"{_configuration["App:FileName"]}.csv";
                var path = Path.Combine(folder, filename );
                return await Task.FromResult<TextReader>(File.OpenText(path));
            }
            catch (FileNotFoundException e)
            {
                throw new Exception($"File associated with the test case couldn't be found: {e.FileName}");
                
            }
        }
    }
    
}