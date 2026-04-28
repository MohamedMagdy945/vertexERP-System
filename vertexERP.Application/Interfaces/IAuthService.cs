using VertexERP.Application.Common.Bases;
using VertexERP.Application.Common.Models;

namespace VertexERP.Application.Identity.Interfaces
{
    public interface IAuthService
    {
        Task<Result<TokenResponse>> RegisterAsync(string username, string email, string password, string? ip, string? device);
        Task<Result<TokenResponse>> LoginAsync(string username, string password, string? ip, string? device);
        Task<Result<TokenResponse>> RefreshTokenAsync(string refreshToken, string? ip, string device);
        Task<Result> LogoutAsync(string refreshToken);

    }
}
