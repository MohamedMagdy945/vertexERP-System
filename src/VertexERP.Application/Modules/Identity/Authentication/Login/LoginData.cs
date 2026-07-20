namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public sealed record RefreshTokenData(
    Guid Id,
    string Email,
    string PasswordHash,
    bool IsActive,
    List<string> Permissions
);

