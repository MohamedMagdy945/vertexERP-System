using VertexERP.Application.Common.Types.Authentication.Contracts;
using VertexERP.Application.Types.Authentication.Models;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed record Response
{
    public required AuthenticatedUser User { get; init; }
    public required AccessTokenInfo AccessToken { get; init; }
}