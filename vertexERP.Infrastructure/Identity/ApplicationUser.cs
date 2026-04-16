using Microsoft.AspNetCore.Identity;
using vertexERP.Domain.Modules.Auth;

namespace vertexERP.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
