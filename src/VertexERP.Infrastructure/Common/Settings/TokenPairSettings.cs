namespace VertexERP.Infrastructure.Common.Settings;

public class TokenPairSettings
{
    public required string SecretKey { get; init; }
    public required string Audience { get; init; }
    public required string Issuer { get; init; }

    public int AccessTokenExpirationInMinutes { get; init; }
    public int RefreshTokenExpirationInDays { get; init; }
}