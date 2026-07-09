namespace VertexERP.Application.Modules.Identity.Permissions.Queries.GetAllPermissions;

public sealed record GetAllPermissionsQueryResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
}