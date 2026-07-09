using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.CreateUser;

public record RefreshCommand(string RefreshToken)
    : IRequest<Result<RefreshResponse>>;

