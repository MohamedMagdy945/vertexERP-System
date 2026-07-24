namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed record Context(Guid UserId, string Email, string PasswordHash, bool IsActive, IReadOnlyCollection<string> Roles
);