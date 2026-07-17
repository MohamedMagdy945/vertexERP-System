using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Commands.Create;

public sealed record Command(
    string Name,
    string Email) : IRequest<Result<Guid>>;