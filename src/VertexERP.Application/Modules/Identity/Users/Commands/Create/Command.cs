using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Commands.Create;

public sealed record Command(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<Result<Response>>;