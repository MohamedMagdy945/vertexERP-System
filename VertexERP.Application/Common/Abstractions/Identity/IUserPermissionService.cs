namespace VertexERP.Application.Common.Abstractions.Identity;

public interface IUserPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(Guid userId, CancellationToken ct = default);
}

