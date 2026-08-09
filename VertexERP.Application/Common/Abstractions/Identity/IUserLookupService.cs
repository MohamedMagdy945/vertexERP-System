namespace VertexERP.Application.Common.Abstractions.Identity;

public interface IUserLookupService
{
    Task<IReadOnlyList<Guid>> GetUserIdsByPermissionAsync(
        string permission,
        CancellationToken ct = default);
}