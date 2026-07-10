namespace VertexERP.Application.Modules.Identity.Users.Queries.GetUsersQuery;

public record GetUsersQueryResponse
{
    public int Id { get; init; }
    public string FullName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public bool IsEnabled { get; init; }
}