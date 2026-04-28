using Microsoft.AspNetCore.Identity;

namespace VertexERP.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
