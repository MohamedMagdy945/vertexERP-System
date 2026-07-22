using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Categories.Commands.Delete;

public sealed record Command(Guid Id) : IRequest<Result<Response>>;