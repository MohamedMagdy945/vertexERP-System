using FluentAssertions;
using Microsoft.Extensions.Options;
using VertexERP.Infrastructure.Common.Settings;
using VertexERP.Infrastructure.Services.Identity;

namespace VertexERP.Infrastructure.Tests.Services.Identity;

public class RefreshTokenServiceTests
{
    private readonly RefreshTokenService _sut;
    private const int ExpirationInDays = 6;

    public RefreshTokenServiceTests()
    {
        var settings = new RefreshTokenSettings
        {
            ExpirationInDays = ExpirationInDays
        };

        var options = Options.Create(settings);
        _sut = new RefreshTokenService(options);
    }

    [Fact]
    public void Generate_WhenCalled_ShouldReturnNonEmptyTokenAndCorrectExpirationDate()
    {
        var expectedExpiresAt = DateTime.UtcNow.AddDays(ExpirationInDays);

        var result = _sut.Generate();

        result.Should().NotBeNull();
        result.Token.Should().NotBeNullOrWhiteSpace();
        result.ExpiresAt.Should().BeCloseTo(expectedExpiresAt, TimeSpan.FromSeconds(2));
    }

    [Fact]
    public void ComputeHash_WhenSameTokenProvided_ShouldReturnSameHash()
    {
        var token = "sample-token-123";

        var hash1 = _sut.ComputeHash(token);
        var hash2 = _sut.ComputeHash(token);

        hash1.Should().Be(hash2);
    }
}