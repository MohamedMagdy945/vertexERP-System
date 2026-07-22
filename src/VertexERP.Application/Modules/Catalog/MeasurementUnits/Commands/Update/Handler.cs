using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Domain.Module.Catalog.Entities;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Commands.Update;

public sealed class Handler(IApplicationDbContext dbContext)
    : IRequestHandler<Command, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var measurementUnit = await dbContext.MeasurementUnits.FindAsync([request.Id], cancellationToken);

        if (measurementUnit is null)
            return Result<Response>.NotFound("Measurement Unit not found.");

        var symbol = MeasurementUnit.FormatSymbol(request.Symbol);

        var exists = await dbContext.MeasurementUnits.AnyAsync(x => x.Id != request.Id && x.Symbol == symbol, cancellationToken);

        if (exists)
            return Result<Response>.Conflict("Measurement Unit symbol already exists.");

        measurementUnit.Update(symbol);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Success(measurementUnit.Adapt<Response>());
    }
}