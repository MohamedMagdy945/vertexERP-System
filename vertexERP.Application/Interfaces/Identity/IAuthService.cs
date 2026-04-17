using vertexERP.Application.Common.Bases;
using vertexERP.Application.Common.Models;

namespace vertexERP.Application.Interfaces.Identity
{
    public interface IAuthService
    {
        Task<Result<TokenResponse>> RegisterAsync(string username, string email, string password, string? ip, string? device);
        Task<Result<TokenResponse>> LoginAsync(string username, string password, string? ip, string? device);
        Task<Result<TokenResponse>> RefreshTokenAsync(string refreshToken, string? ip, string device);
        Task<Result> LogoutAsync(string refreshToken);

    }
}
