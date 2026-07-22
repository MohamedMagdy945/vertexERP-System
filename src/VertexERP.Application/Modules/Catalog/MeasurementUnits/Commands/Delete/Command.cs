using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Commands.Delete;

public sealed record Command(Guid Id) : IRequest<Result<Response>>;