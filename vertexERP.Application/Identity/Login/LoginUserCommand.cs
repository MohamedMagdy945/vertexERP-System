using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Bases;
using VertexERP.Application.Common.Models;
using VertexERP.Application.Identity.Interfaces;

namespace VertexERP.Application.Identity.Login
{
    public record LoginUserCommand(string Username, string Password)
        : IRequest<Response<TokenResponse>>;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<TokenResponse>>
    {
        private readonly IAuthService _authService;
        private readonly ILogger<LoginUserCommandHandler> _logger;
        public LoginUserCommandHandler(
            IAuthService authService,
            ILogger<LoginUserCommandHandler> logger
            )
        {
            _authService = authService;
            _logger = logger;
        }
        public async Task<Response<TokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            var result = await _authService.LoginAsync(
                request.Username,
                request.Password);

            if (!result.IsSuccess)
            {
                _logger.LogError(
                    "Register attempt failed. Username: {UserName}, Email: {Email}",
                    request.Username,
                    request.Password);

                return ResponseHandler.Failure<TokenResponse>(result.Error ?? "User Login failed");
            }

            var tokenResponse = result.Data;

            _logger.LogInformation("Login successfully {UserId}", tokenResponse!.UserId);

            return ResponseHandler.Success(tokenResponse!, "User Login successfully");
        }
    }
}
