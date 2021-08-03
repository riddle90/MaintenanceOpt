using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RoutingEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappingService : Controller
    {
        private readonly ILogger logger;

        public MappingService(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger("Mapping Service");
        }

        [HttpPost]
        public async Task Run(string filename)
        {
            await Task.CompletedTask;
        }
    }
}