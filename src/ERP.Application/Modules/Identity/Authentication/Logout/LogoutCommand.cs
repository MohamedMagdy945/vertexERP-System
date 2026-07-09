using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Logout;

public record LogoutCommand(string RefreshToken)
    : IRequest<Result<LogoutResponse>>;

