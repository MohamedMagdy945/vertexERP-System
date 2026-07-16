namespace VertexERP.Infrastructure.Identity.Settings;

public sealed class AccessTokenSettings
{
    public required string SecretKey { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public int ExpirationInMinutes { get; init; }
}