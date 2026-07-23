using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using VertexERP.Application.Common.Abstractions.Cache;


namespace VertexERP.Infrastructure.Services.Cache;

public sealed class MemoryUserPermissionCache(IDistributedCache cache) : IUserPermissionCache
{
    private const string Prefix = "user-permissions:";

    public async Task<HashSet<string>?> GetAsync(Guid userId, CancellationToken ct = default)
    {
        var data = await cache.GetStringAsync(GetKey(userId), ct);

        if (data is null)
            return null;

        return JsonSerializer.Deserialize<HashSet<string>>(data);
    }


    public async Task SetAsync(Guid userId, HashSet<string> permissions, CancellationToken ct = default)
    {
        var data = JsonSerializer.Serialize(permissions);

        await cache.SetStringAsync(GetKey(userId), data,
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromHours(2),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(8)
            }, ct);
    }

    public async Task RemoveAsync(Guid userId, CancellationToken ct = default)
    {
        await cache.RemoveAsync(GetKey(userId), ct);
    }
    private static string GetKey(Guid userId)
        => $"{Prefix}{userId}";
}