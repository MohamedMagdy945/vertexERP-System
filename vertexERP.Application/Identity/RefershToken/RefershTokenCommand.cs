using MediatR;
using Microsoft.Extensions.Logging;
using VertexERP.Application.Common.Bases;
using VertexERP.Application.Common.Models;
using VertexERP.Application.Identity.Interfaces;

namespace VertexERP.Application.Identity.RefershToken
{
    public record RefershTokenCommand(string Username, string RefershToken)
        : IRequest<Response<TokenResponse>>;

    public class RefershTokenCommandHandler : IRequestHandler<RefershTokenCommand, Response<TokenResponse>>
    {
        private readonly IAuthService _authService;
        private readonly ILogger<RefershTokenCommandHandler> _logger;
        public RefershTokenCommandHandler(
            IAuthService authService,
            ILogger<RefershTokenCommandHandler> logger
            )
        {
            _authService = authService;
            _logger = logger;
        }
        public async Task<Response<TokenResponse>> Handle(RefershTokenCommand request, CancellationToken cancellationToken)
        {

            var result = await _authService.RefreshTokenAsync(
                request.Username,
                request.RefershToken);

            if (!result.IsSuccess)
            {
                _logger.LogError(
                    "Refresh Token failed . Username: {UserName}, RefershToken: {RefershToken}",
                    request.Username,
                    request.RefershToken);

                return ResponseHandler.Failure<TokenResponse>(result.Error ?? "RefershToken failed");
            }

            var tokenResponse = result.Data;

            _logger.LogInformation("Refersh Token successfully {UserId}", tokenResponse!.UserId);

            return ResponseHandler.Success(tokenResponse!, "Refersh Token successfully");
        }
    }
}
