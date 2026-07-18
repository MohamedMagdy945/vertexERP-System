namespace VertexERP.Application.Common.Models.Identity;

public record UserTokenClaims(Guid UserId, string Email, IEnumerable<string>? Permissions);
