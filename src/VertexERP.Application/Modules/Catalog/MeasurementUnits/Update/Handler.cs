using Microsoft.EntityFrameworkCore;
using VertexERP.Application.Common.Abstractions.Endpoint;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Application.Shared.Results;
using VertexERP.Domain.Module.Catalog.Entities;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Update;

public sealed class Handler(IAppDbContext dbContext) : IHandler
{
    public async Task<Result<Response>> HandleAsync(Request request, CancellationToken ct)
    {
        var symbol = MeasurementUnit.FormatSymbol(request.Symbol);

        var affected = await dbContext.MeasurementUnits
            .Where(x => x.Id == request.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(x => x.Symbol, symbol), ct);

        if (affected == 0)
            return Result<Response>.NotFound("Measurement unit not found.");

        return Result<Response>.Success(new Response(request.Id, symbol));
    }
}