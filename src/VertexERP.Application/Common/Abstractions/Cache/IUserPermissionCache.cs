namespace VertexERP.Application.Common.Abstractions.Cache;

public interface IUserPermissionCache
{
    Task<IReadOnlyList<string>?> GetAsync(Guid userId, CancellationToken ct = default);
    Task SetAsync(Guid userId, IReadOnlyList<string> permissions, CancellationToken ct = default);
    Task RemoveAsync(Guid userId, CancellationToken ct = default);
}