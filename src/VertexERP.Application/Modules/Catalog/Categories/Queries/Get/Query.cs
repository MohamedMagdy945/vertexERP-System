using Mediator;
using VertexERP.Shared.Pagination;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Categories.Queries.Get;

public sealed record Query : SearchablePageQuery, IRequest<Result<Page<Response>>>;