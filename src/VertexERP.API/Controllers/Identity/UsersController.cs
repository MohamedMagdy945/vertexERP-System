using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Modules.Identity.Users.Commands.CreateUser;
using VertexERP.Shared.Constants;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers.Identity;

public class UsersController : AppControllerBase
{
    [HttpPost("CreateUser")]
    [Authorize(Policy = PermissionNames.Users.Create)]
    [ProducesResponseType(typeof(Result<CreateUserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateUser(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(command, cancellationToken);

        return ApiResponse(response);
    }
}

