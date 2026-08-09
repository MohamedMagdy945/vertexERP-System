namespace VertexERP.Application.Shared.Pagination;

public record SearchablePageRequest : PageRequest
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