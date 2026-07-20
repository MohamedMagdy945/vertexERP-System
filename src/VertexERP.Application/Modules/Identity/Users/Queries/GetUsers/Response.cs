namespace VertexERP.Application.Modules.Identity.Users.Queries.GetUsers;

public sealed record Response(
    Guid Id,
    string FullName,
    string FirstName,
    string LastName,
    bool IsActive,
    DateTime CreatedAt);