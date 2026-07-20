using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Commands.Create;

public sealed record Command(
    string FullName,
    string Email) : IRequest<Result<Response>>;