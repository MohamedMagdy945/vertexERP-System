using VertexERP.Application.Common.Bases;
using VertexERP.Application.Common.Models;

namespace VertexERP.Application.Identity.Interfaces
{
    public interface IAuthService
    {
        Task<Result<TokenResponse>> RegisterAsync(string username, string email, string password);
        Task<Result<TokenResponse>> LoginAsync(string username, string password);
        Task<Result<TokenResponse>> RefreshTokenAsync(string username, string refreshToken);
        Task<Result<LogoutResponse>> LogoutAsync(string refreshToken);

    }
}
