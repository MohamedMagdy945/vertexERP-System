namespace VertexERP.Shared.Pagination;

public record SearchablePageQuery : PageQuery
{
    private readonly string? _searchTerm;

    public string? SearchTerm
    {
        get => _searchTerm;
        init
        {
            _searchTerm = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}