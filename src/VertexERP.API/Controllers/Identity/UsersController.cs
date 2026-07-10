using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Modules.Identity.Users.Commands.CreateUser;
using VertexERP.Application.Modules.Identity.Users.Queries.GetUserById;
using VertexERP.Application.Modules.Identity.Users.Queries.GetUsersQuery;
using VertexERP.Shared.Constants;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers.Identity;

public class UsersController : AppControllerBase
{


    [HttpGet("GetUsers")]
    [Authorize(Policy = PermissionNames.Users.View)]
    [ProducesResponseType(typeof(Result<List<GetUsersQueryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery command, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(command, cancellationToken);
        return ApiResponse(response);
    }

    [HttpGet("GetUserById/{id}")]
    [Authorize(Policy = PermissionNames.Users.View)]
    [ProducesResponseType(typeof(Result<GetUsersQueryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetUserByIdQuery(id), cancellationToken);
        return ApiResponse(response);
    }


    [HttpPost("CreateUser")]
    [Authorize(Policy = PermissionNames.Users.Create)]
    [ProducesResponseType(typeof(Result<CreateUserCommandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateUser(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(command, cancellationToken);

        return ApiResponse(response);
    }


}

