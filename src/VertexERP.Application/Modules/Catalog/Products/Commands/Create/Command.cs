using Mediator;
using Microsoft.AspNetCore.Http;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Commands.Create;

public sealed record Command(string Name, string Code, decimal CostPrice, decimal SellingPrice,
    Guid CategoryId, Guid UnitId, string? Barcode, string? Description, IReadOnlyList<IFormFile>? Images)
    : IRequest<Result<Response>>;
