using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Commands.Update;

public sealed record Command(Guid Id, string Symbol)
    : IRequest<Result<Response>>;