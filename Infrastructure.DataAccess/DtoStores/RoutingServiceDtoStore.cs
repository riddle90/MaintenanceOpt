using System;
using System.Collections;
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
    public class StopDtoStore : IStopDtoStore
    {
        private readonly IConfiguration _configuration;

        public StopDtoStore(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<ICollection<MaintenanceStopDto>> GetMaintenanceStopDtos()
        {
            using (TextReader reader = await FileReader.GetReader(this._configuration["App:Filename"]))
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true,
                };
                
                configuration.IgnoreBlankLines = true;
                using (var csv = new CsvReader(reader, configuration))
                {
                    csv.Context.RegisterClassMap<StopDataMapping>();
                    return csv.GetRecords<MaintenanceStopDto>().ToList();
                }
            }
        }

       
       
    }
    
}