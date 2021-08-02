
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RoutingEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineServiceController : Controller
    {
        public readonly ILogger logger;
        public EngineServiceController(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger("RoutingEngine");

        }

        [HttpPost]
        public async Task<string> Run(int scenarioId)
        {
            logger.LogDebug($"Starting Engine Service {scenarioId}");
            await Task.Delay(1);
            return "Job Done";
        }
    }
}