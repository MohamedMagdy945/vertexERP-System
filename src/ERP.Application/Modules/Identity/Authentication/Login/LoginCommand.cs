using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public record LoginCommand(string Email, string Password)
    : IRequest<Result<LoginResponse>>;

