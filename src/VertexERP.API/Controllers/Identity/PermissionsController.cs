using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VertexERP.Application.Modules.Identity.Permissions.Queries.GetAllPermissions;
using VertexERP.Shared.Constants;
using VertexERP.Shared.Results;

namespace VertexERP.API.Controllers.Identity;

[Tags("Identity")]
public class PermissionsController : AppControllerBase
{
    [HttpGet("GetAllPermissions")]
    [Authorize(Policy = PermissionNames.Permissions.View)]
    [ProducesResponseType(typeof(Result<List<GetAllPermissionsQueryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPermissions()
    {
        var response = await Mediator.Send(new GetAllPermissionsQuery());
        return Ok(response);

    }
}
