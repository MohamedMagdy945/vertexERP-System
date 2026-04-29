using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CreateUserCommandHandler> _logger;
        public CreateUserCommandHandler(
            IAuthService authService,
            ILogger<CreateUserCommandHandler> logger
            )
        {
            _authService = authService;
            _logger = logger;
        }
        public async Task<Response<TokenResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var result = await _authService.RegisterAsync(
                request.UserName,
                request.Email,
                request.Password);

            if (!result.IsSuccess)
            {
                _logger.LogError(
                    "Register attempt failed. Username: {UserName}, Email: {Email}",
                    request.UserName,
                    request.Email);

                return ResponseHandler.Failure<TokenResponse>(result.Error ?? "User registration failed");
            }

            var tokenResponse = result.Data;

            _logger.LogInformation("User registered successfully {UserId}", tokenResponse!.UserId);

            return ResponseHandler.Success(tokenResponse!, "User registered successfully");
        }
    }
}