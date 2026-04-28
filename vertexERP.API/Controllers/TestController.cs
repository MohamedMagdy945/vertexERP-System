using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace VertexERP.API.Controllers
{
    [ApiVersion("1.0")]
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
        [ProducesResponseType(typeof(UserDto), 200)]
        public IActionResult Get()
        {
            logger.LogInformation("Health check endpoint was called.");

            return Ok(new UserDto
            {
                Id = 1,
                UserId = 1,
                Name = "API is working"
            });
        }

    }
}
