using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public record Command(string Email, string Password)
    : IRequest<Result<Response>>;

