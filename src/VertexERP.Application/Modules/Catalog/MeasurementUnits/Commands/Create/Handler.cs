using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Catalog.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Commands.Create;

public sealed class Handler(IApplicationDbContext dbContext, ILogger<Handler> logger)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var symbol = MeasurementUnit.FormatSymbol(request.Symbol);

        var exists = await dbContext.MeasurementUnits.AnyAsync(x => x.Symbol == symbol, cancellationToken);

        if (exists)
            return Result<Response>.Conflict("Measurement Unit already exists.");

        var measurementUnit = new MeasurementUnit(symbol);

        dbContext.MeasurementUnits.Add(measurementUnit);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Created(measurementUnit.Adapt<Response>());
    }
}