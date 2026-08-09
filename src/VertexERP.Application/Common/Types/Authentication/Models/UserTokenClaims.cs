namespace VertexERP.Application.Common.Types.Authentication.Models;

public sealed record UserTokenClaims(Guid UserId, string Email);