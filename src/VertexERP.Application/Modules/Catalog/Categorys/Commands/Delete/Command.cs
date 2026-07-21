using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Categorys.Commands.Delete;

public sealed record Command(Guid Id) : IRequest<Result<Response>>;