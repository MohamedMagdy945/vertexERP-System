namespace VertexERP.Application.Modules.Identity.Users.Get;

public sealed record Response(
    Guid Id,
    string Name,
    string Email,
    bool IsActive,
    DateTime CreatedAt);