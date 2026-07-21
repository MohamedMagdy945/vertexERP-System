using Mediator;
using VertexERP.Application.Modules.Inventory.Categorys.Command.Create;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Categorys.Queries.GetById;

public sealed record Query(Guid Id) : IRequest<Result<Response>>;

