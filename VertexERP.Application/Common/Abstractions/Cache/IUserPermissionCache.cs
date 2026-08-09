namespace VertexERP.Application.Common.Abstractions.Cache;

public interface IUserPermissionCache
{
    Task<HashSet<string>?> GetAsync(Guid userId, CancellationToken ct = default);
    Task SetAsync(Guid userId, HashSet<string> permissions, CancellationToken ct = default);
    Task RemoveAsync(Guid userId, CancellationToken ct = default);
}