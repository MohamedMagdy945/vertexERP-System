using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Abstractions.Persistence;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Queries.GetById;

public sealed class Handler(IApplicationDbContext dbContext, ILogger<Handler> logger)
    : IRequestHandler<Query, Result<Response>>
{
    public async ValueTask<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var measurementUnit = await dbContext.MeasurementUnits.AsNoTracking().Where(x => x.Id == request.Id)
                                .ProjectToType<Response>().FirstOrDefaultAsync(cancellationToken);

        if (measurementUnit is null)
            return Result<Response>.NotFound("Category not found.");

        return Result<Response>.Success(measurementUnit);
    }
}