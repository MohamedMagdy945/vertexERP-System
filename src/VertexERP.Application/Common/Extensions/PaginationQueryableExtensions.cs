using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Shared.Pagination;

public static class PaginationQueryableExtensions
{
    public static async Task<Page<T>> ToPageAsync<T>(
        this IQueryable<T> query,
        PageRequest request,
        CancellationToken ct = default)
    {
        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(ct);

        return Page<T>.Create(
            items,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }
}