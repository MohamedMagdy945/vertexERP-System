using Mediator;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Refresh;

public record Command(string RefreshToken)
    : IRequest<Result<Response>>;

