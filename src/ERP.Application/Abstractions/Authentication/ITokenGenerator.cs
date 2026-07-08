using VertexERP.Application.Models.Authentication;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Abstractions.Authentication;

public interface ITokenGenerator
{
    TokenPair GenerateTokenPair(User user, IEnumerable<string>? permissions);
    string GenerateAccessToken(User user, IEnumerable<string>? permissions);
    string GenerateRefreshToken();
    string HashToken(string token);
}

