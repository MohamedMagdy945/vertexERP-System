using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Categorys.Command.Update;

public sealed record Command(Guid Id, string Name, string? Description)
    : IRequest<Result<Response>>;