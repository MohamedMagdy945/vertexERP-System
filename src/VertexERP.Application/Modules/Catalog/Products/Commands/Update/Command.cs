using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Commands.Update;

public sealed record Command(Guid Id, string Name, string Code, decimal CostPrice
    , decimal SellingPrice, Guid CategoryId, Guid UnitId, string? Description)
    : IRequest<Result<Response>>;
