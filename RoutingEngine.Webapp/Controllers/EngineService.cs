
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UseCases;

namespace RoutingEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineServiceController : Controller
    {
        private readonly IRoutingEngine _routingEngine;
        public readonly ILogger logger;
        public EngineServiceController(ILoggerFactory loggerFactory, IRoutingEngine routingEngine)
        {
            _routingEngine = routingEngine;
            this.logger = loggerFactory.CreateLogger("RoutingEngine");
        }

        [HttpPost]
        public async Task<string> Run()
        {
            await this._routingEngine.Run();
            return "Job Done";
        }
    }
}