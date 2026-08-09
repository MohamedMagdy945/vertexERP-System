namespace VertexERP.Application.Shared.Pagination;

public sealed class Page<T>
{
    public IReadOnlyList<T> Items { get; }

    public int TotalCount { get; }

    public int PageNumber { get; }

    public int PageSize { get; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    private Page(
        IReadOnlyList<T> items,
        int totalCount,
        int pageNumber,
        int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public static Page<T> Create(
        IReadOnlyList<T> items,
        int totalCount,
        int pageNumber,
        int pageSize)
    {
        return new(items, totalCount, pageNumber, pageSize);
    }

    public static Page<T> Create(
        IReadOnlyList<T> items,
        int totalCount,
        PageRequest request)
    {
        return new(
            items,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }
}