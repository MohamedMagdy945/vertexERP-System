namespace VertexERP.Application.Modules.Identity.Permissions.Queries.GetAllPermissions;

public record GetAllPermissionsQueryResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
}