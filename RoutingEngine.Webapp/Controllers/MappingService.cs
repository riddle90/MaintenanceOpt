using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UseCases;

namespace RoutingEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappingService : Controller
    {
        private readonly IMappingMode _mappingMode;
        private readonly ILogger logger;

        public MappingService(ILoggerFactory loggerFactory, IMappingMode mappingMode)
        {
            _mappingMode = mappingMode;
            this.logger = loggerFactory.CreateLogger("Mapping Service");
        }

        [HttpPost]
        public async Task Run(string apiKey)
        {
            await _mappingMode.Run(apiKey);
            logger.LogDebug("Distance Matrix Written To File");
        }
    }
}