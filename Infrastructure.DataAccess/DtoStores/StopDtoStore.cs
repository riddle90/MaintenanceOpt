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
            var fileName = "Mainteny_Sample_Data_Elevators_2.csv"; //this._configuration["Filename"];
            using (TextReader reader = await FileReader.GetReader(fileName))
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true,
                    IgnoreBlankLines = true,
                    ShouldSkipRecord = (record) => record.Record.All(field => string.IsNullOrWhiteSpace(field)),
                };
                
                using (var csv = new CsvReader(reader, configuration))
                {
                    csv.Context.RegisterClassMap<StopDataMapping>();
                    return csv.GetRecords<MaintenanceStopDto>().ToList();
                }
            }
        }

       
       
    }
    
}