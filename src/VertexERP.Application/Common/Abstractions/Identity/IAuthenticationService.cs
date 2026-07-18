using VertexERP.Application.Common.Models.Identity;

namespace VertexERP.Application.Common.Abstractions.Identity;

public interface IAuthenticationService
{
    public TokenPair CreateSessionAsync(UserTokenClaims claims, CancellationToken cancellationToken);
}
