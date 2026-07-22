using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Commands.Create;

public sealed record Command(string Symbol) : IRequest<Result<Response>>;

