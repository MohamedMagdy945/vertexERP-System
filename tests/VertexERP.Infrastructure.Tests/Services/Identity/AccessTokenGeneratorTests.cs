using FluentAssertions;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using VertexERP.Application.Common.Types.Authentication.Models;
using VertexERP.Infrastructure.Common.Settings;
using VertexERP.Infrastructure.Services.Identity;

namespace VertexERP.Infrastructure.Tests.Services.Identity;

public class AccessTokenGeneratorTests
{
    private readonly AccessTokenGenerator _sut;
    private readonly AccessTokenSettings _settings;

    public AccessTokenGeneratorTests()
    {
        _settings = new AccessTokenSettings
        {
            SecretKey = "super-secret-key-that-is-at-least-32-bytes-long!",
            Issuer = "VertexERP",
            Audience = "VertexERP-App",
            ExpirationInMinutes = 15
        };

        var options = Options.Create(_settings);
        _sut = new AccessTokenGenerator(options);
    }

    [Fact]
    public void Generate_WhenCalled_ShouldReturnValidTokenAndCorrectExpiration()
    {
        var userId = Guid.NewGuid();
        var userClaims = new UserTokenClaims(userId, "test@vertex.com");
        var expectedExpiresAt = DateTime.UtcNow.AddMinutes(_settings.ExpirationInMinutes);

        var result = _sut.Generate(userClaims);

        result.Should().NotBeNull();
        result.Token.Should().NotBeNullOrWhiteSpace();
        result.ExpiresAt.Should().BeCloseTo(expectedExpiresAt, TimeSpan.FromSeconds(2));

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(result.Token);

        jwt.Issuer.Should().Be(_settings.Issuer);
        jwt.Audiences.Should().Contain(_settings.Audience);
        jwt.Subject.Should().Be(userId.ToString());
        jwt.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Email && c.Value == userClaims.Email);
        jwt.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Jti);
    }
}