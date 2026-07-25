using VertexERP.Application.Shared.Pagination;

namespace VertexERP.Application.Modules.Identity.Users.Get;

public sealed record UserResponse(
    Guid Id,
    string Name,
    string Email,
    bool IsActive,
    DateTime CreatedAt);

public sealed record Response(Page<UserResponse> Users);