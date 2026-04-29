using Microsoft.AspNetCore.Mvc;

namespace VertexERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }
        [HttpGet("OK")]
        public ActionResult Test()
        {
            return Ok("done");
        }
    }
}
