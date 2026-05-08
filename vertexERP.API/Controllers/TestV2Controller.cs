using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Common.Bases;

namespace VertexERP.API.Controllers
{
    [ApiVersion("2.0")]
    public class TestV2Controller : AppControllerBase
    {

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(Response<DTOOO>), 200)]
        public ActionResult<Response<DTOOO>> Index()
        {
            throw new NotImplementedException();
        }
        [HttpGet("OK")]
        [Authorize]

        public ActionResult Test()
        {
            return Ok("done");
        }
    }
}
