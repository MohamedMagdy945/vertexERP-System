using Microsoft.EntityFrameworkCore;
using VertexERP.Infrastructure.Identity.Entities;
using VertexERP.Infrastructure.Persistence.DbContext;

namespace VertexERP.Infrastructure.Identity.Identity
{
    public class RefreshTokenService
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SaveRefreshTokenAsync(int userId, string refreshToken, DateTime expirationDate)
        {
            var refreshTokenEntity = new RefreshToken
            {
                UserId = userId,
                TokenHash = refreshToken,
                ExpiresAt = expirationDate
            };
            _context.RefreshTokens.Add(refreshTokenEntity);

            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken)
        {
            var hashedToken = refreshToken;
            return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == hashedToken);
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }
    }
}
