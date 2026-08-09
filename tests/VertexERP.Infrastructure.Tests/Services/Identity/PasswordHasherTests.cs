using FluentAssertions;
using VertexERP.Infrastructure.Services.Identity;

namespace VertexERP.Infrastructure.Tests.Services.Identity;

public class PasswordHasherTests
{
    private readonly PasswordHasher _sut = new();

    [Fact]
    public void Verify_WhenPasswordIsCorrect_ShouldReturnTrue()
    {
        // Arrange
        var password = "MySecurePassword123!";
        var hash = _sut.Hash(password);

        // Act
        var result = _sut.Verify(password, hash);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Verify_WhenPasswordIsWrong_ShouldReturnFalse()
    {
        // Arrange
        var hash = _sut.Hash("MySecurePassword123!");

        // Act
        var result = _sut.Verify("WrongPassword!", hash);

        // Assert
        result.Should().BeFalse();
    }
}