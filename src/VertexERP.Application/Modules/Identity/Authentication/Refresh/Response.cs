using VertexERP.Application.Types.Authentication.Contracts;
using VertexERP.Application.Types.Authentication.Models;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;


public sealed record Response(
    AuthenticatedUser User,
    AccessTokenInfo AccessToken);

