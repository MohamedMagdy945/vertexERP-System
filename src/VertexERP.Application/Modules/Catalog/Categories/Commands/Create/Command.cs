using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Catalog.Categories.Commands.Create;

public sealed record Command(string Name, string? Description) : IRequest<Result<Response>>;

