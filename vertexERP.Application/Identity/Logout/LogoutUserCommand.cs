using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Bases;
using VertexERP.Application.Identity.Interfaces;
using VertexERP.Application.Identity.Login;

namespace VertexERP.Application.Identity.Logout
{
    public record LogoutUserCommand(string Username, string RefreshToken)
          : IRequest<Response<bool>>;

    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, Response<bool>>
    {
        private readonly IAuthService _authService;
        private readonly ILogger<LoginUserCommandHandler> _logger;
        public LogoutUserCommandHandler(
            IAuthService authService,
            ILogger<LoginUserCommandHandler> logger
            )
        {
            _authService = authService;
            _logger = logger;
        }
        public async Task<Response<bool>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {

            var result = await _authService.LogoutAsync(
              request.RefreshToken);

            if (!result.IsSuccess)
            {
                _logger.LogError(
                    "Logout attempt failed. Username: {Username}",
                    request.Username);

                return ResponseHandler.Failure<bool>(
                    result.Error ?? "User logout failed");
            }

            _logger.LogInformation(
                "User logout successfully. Username: {Username}",
                request.Username);

            return ResponseHandler.Success(
                true,
                "User logout successfully");
        }

    }
}