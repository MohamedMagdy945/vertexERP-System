using Mapster;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Catalog.Entities;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Create;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var symbol = MeasurementUnit.FormatSymbol(request.Symbol);

        var measurementUnit = new MeasurementUnit(symbol);

        dbContext.MeasurementUnits.Add(measurementUnit);
        await dbContext.SaveChangesAsync(ct);

        return Result<Response>.Created(measurementUnit.Adapt<Response>());
    }
}