using MediatR;
using VertexERP.Application.Common.Bases;
using VertexERP.Application.Common.Models;
using VertexERP.Application.Identity.Interfaces;

namespace VertexERP.Application.Identity.Register
{
    public record CreateUserCommand(string UserName, string Email, string Password,
    string ConfirmPassword) : IRequest<Response<TokenResponse>>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<TokenResponse>>
    {
        private readonly IAuthService _authService;
        public CreateUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<Response<TokenResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var result = await _authService.RegisterAsync(
                request.UserName,
                request.Email,
                request.Password);

            if (!result.IsSuccess)
                return ResponseHandler.Failure<TokenResponse>(result.Error ?? "User registration failed");

            var tokenResponse = result.Data;

            return ResponseHandler.Success(tokenResponse!, "User registered successfully");
        }
    }
}