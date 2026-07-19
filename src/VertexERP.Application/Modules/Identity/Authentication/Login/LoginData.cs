namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed record LoginData(
    Guid Id,
    string Email,
    string PasswordHash,
    bool IsActive,
    List<string> Permissions
);

