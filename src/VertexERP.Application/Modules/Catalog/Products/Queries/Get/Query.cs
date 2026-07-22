using Mediator;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Queries.Get;

public sealed record Query : SearchablePageQuery, IRequest<Result<Page<Response>>>
{
    private readonly string? _code;

    public string? Code
    {
        get => _code;
        init => _code = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}