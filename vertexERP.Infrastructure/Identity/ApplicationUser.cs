using Microsoft.AspNetCore.Identity;

namespace vertexERP.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
