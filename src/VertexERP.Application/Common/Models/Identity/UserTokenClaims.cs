namespace VertexERP.Application.Common.Models.Identity;

public sealed record UserTokenClaims(Guid UserId, string Email, IEnumerable<string> Roles);