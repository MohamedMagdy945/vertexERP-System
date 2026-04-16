using vertexERP.Application.Common.Bases;
using vertexERP.Application.Common.Models;

namespace vertexERP.Application.Interfaces.Identity
{
    public interface IAuthenticationService
    {
        Task<Result<TokenResponse>> RegisterAsync(string username, string password, string ip, string device);
        Task<Result<TokenResponse>> LoginAsync(string username, string password, string ip, string device);
        Task<Result<TokenResponse>> RefreshTokenAsync(string refreshToken, string ip);
        Task<Result> LogoutAsync(string refreshToken);

    }
}
