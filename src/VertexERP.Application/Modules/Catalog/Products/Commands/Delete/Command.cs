using Mediator;
using VertexERP.Application.Modules.Catalog.MeasurementUnits.Commands.Delete;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Products.Commands.Delete;

public sealed record Command(Guid Id) : IRequest<Result<Response>>;