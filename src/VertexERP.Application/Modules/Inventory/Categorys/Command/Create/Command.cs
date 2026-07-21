using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Categorys.Command.Create;

public sealed record Query(string Name, string? Description) : IRequest<Result<Response>>;

