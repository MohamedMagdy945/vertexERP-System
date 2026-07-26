using VertexERP.Application.Types.Authentication.Contracts;
using VertexERP.Application.Types.Authentication.Models;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed record Response
{
    public AuthenticatedUser User { get; init; } = default!;
    public AccessTokenInfo AccessToken { get; init; } = default!;
}