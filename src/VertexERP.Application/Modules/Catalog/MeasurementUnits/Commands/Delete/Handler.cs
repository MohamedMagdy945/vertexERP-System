using Mediator;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Commands.Delete;

public sealed class Handler(IApplicationDbContext dbContext, ILogger<Handler> logger)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var measurementUnit = await dbContext.MeasurementUnits.FindAsync([request.Id], cancellationToken);

        if (measurementUnit is null)
            return Result<Response>.NotFound("Measurement Unit not found.");

        dbContext.MeasurementUnits.Remove(measurementUnit);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Success(new Response(measurementUnit.Id));
    }
}