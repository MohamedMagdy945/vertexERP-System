using Microsoft.Extensions.Caching.Memory;
using VertexERP.Application.Common.Abstractions.Cache;

namespace VertexERP.Infrastructure.Services.Cache;

public sealed class MemoryUserPermissionCache(IMemoryCache cache) : IUserPermissionCache
{
    private const string Prefix = "user-permissions:";

    public Task<HashSet<string>?> GetAsync(Guid userId, CancellationToken ct = default)
    {
        cache.TryGetValue(GetKey(userId), out HashSet<string>? permissions);

        return Task.FromResult(permissions);
    }

    public Task SetAsync(Guid userId, HashSet<string> permissions, CancellationToken ct = default)
    {
        cache.Set(
            GetKey(userId),
            permissions,
            new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromHours(2),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(8)
            });

        return Task.CompletedTask;
    }


    public Task RemoveAsync(Guid userId, CancellationToken ct = default)
    {
        cache.Remove(GetKey(userId));

        return Task.CompletedTask;
    }


    private static string GetKey(Guid userId)
        => $"{Prefix}{userId}";
}