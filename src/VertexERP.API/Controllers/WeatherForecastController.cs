using Mediator;
using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Modules.Identity.Users.Commands.Create;

namespace VertexERP.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ISender sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Post(Command command)
    {
        var result = await sender.Send(command);

        return Ok(result);
    }
}
