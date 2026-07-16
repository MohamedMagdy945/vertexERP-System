namespace VertexERP.Application.Common.Models.Identity;

public record UserTokenClaims(string UserId, string Email, IEnumerable<string>? Permissions);
