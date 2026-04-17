namespace vertexERP.Infrastructure.Common.Models
{
    public class TokenPair
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string RefreshTokenHash { get; set; } = null!;
        public DateTime AccessTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public string? Ip { get; set; } = null!;
        public string? Device { get; set; } = null!;
    }
}
