namespace VertexERP.Shared.Pagination;

public record PageQuery
{
    private const int MaxPageSize = 50;
    private readonly int _pageNumber = 1;
    private readonly int _pageSize = 10;

    public int PageNumber
    {
        get => _pageNumber;
        init => _pageNumber = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value < 1 ? 10 : (value > MaxPageSize ? MaxPageSize : value);
    }
}

