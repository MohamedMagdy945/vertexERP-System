using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Categories.Commands.Update;

public sealed record Command(Guid Id, string Name, string? Description)
    : IRequest<Result<Response>>;