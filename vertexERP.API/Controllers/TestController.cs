using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace VertexERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> logger;

        public TestController(ILogger<TestController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            Log.Information("🔥 Test log from application");
            return Ok("Done");

        }
    }
}
