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
        var exists = await dbContext.MeasurementUnits.AnyAsync(x => x.Name == request.Name || x.Symbol == request.Symbol, cancellationToken);
        if (exists)
            return Result<Response>.Conflict("Unit already exists.");

        var measurementUnit = new MeasurementUnit(request.Name, request.Symbol);

        dbContext.MeasurementUnits.Add(measurementUnit);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Created(measurementUnit.Adapt<Response>());
    }
}