namespace VertexERP.Application.Types.Authentication.Models;

public sealed record UserTokenClaims(
    Guid UserId, string Email,
    IEnumerable<string> Roles);