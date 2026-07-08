using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Login;

public class LoginCommandHandler
    : IRequestHandler<LoginCommand, Result<LoginResponse>>
{

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

