using VertexERP.Application.Types.Authentication.Models;

namespace VertexERP.Application.Common.Abstractions.Identity;

public interface IRefreshTokenService
{
    RefreshTokenInfo Generate();
    string ComputeHash(string token);
}