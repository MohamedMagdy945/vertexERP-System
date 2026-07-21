using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Categorys.Command.Delete;

public sealed record Command(Guid Id) : IRequest<Result<Response>>;