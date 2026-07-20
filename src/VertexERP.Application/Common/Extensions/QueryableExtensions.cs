namespace VertexERP.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TSource> ApplyPagination<TSource>(
        this IQueryable<TSource> query, int pageNumber, int pageSize)
    {
        return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}