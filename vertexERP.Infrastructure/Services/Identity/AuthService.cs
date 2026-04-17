using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using vertexERP.Application.Common.Authorization;
using vertexERP.Application.Common.Bases;
using vertexERP.Application.Common.Models;
using vertexERP.Application.Interfaces.Identity;
using vertexERP.Infrastructure.Identity;

namespace vertexERP.Infrastructure.Services.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenGenerator _tokenGenerator;
        private readonly PermissionService _permissionService;
        private readonly RefreshTokenService _refreshTokenService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            JwtTokenGenerator tokenGenerator,
            PermissionService permissionService,
            RefreshTokenService refreshTokenService
            )
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _permissionService = permissionService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Result<TokenResponse>> RegisterAsync(string username, string email, string password, string? ip, string? device)
        {
            var existingUser = await _userManager.FindByNameAsync(username)
                         ?? await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
                return Result<TokenResponse>.Failure("User already exists");

            var user = new ApplicationUser
            {
                UserName = username,
                Email = email
            };

            var createResult = await _userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                return Result<TokenResponse>.Failure(
                    string.Join(", ", createResult.Errors.Select(e => e.Description)));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, Roles.User);

            if (!roleResult.Succeeded)
            {
                return Result<TokenResponse>.Failure(
                    string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }

            var roles = await _userManager.GetRolesAsync(user);


            var permissions = await _permissionService.GetUserPermissionsAsync(user.Id);


            var tokenResponse = _tokenGenerator.GenerateTokenPair(user, permissions);

            await _refreshTokenService.SaveRefreshTokenAsync(
                             user.Id,
                             tokenResponse.RefreshToken,
                             tokenResponse.RefreshTokenExpiration);

            return Result<TokenResponse>.Success(tokenResponse);
        }
        public async Task<Result<TokenResponse>> LoginAsync(string username, string password, string? ip, string? device)
        {

            var user = await _userManager.FindByNameAsync(username)
                         ?? await _userManager.FindByEmailAsync(username);

            if (user == null)
                return Result<TokenResponse>.Failure("Invalid credentials");

            var isValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isValid)
                return Result<TokenResponse>.Failure("Invalid credentials");

            var permissions = await _permissionService.GetUserPermissionsAsync(user.Id);

            var tokenResponse = _tokenGenerator.GenerateTokenPair(user, permissions);

            await _refreshTokenService.SaveRefreshTokenAsync(
                user.Id,
                tokenResponse.RefreshToken,
                tokenResponse.RefreshTokenExpiration);

            return Result<TokenResponse>.Success(tokenResponse);
        }

        public async Task<Result<TokenResponse>> RefreshTokenAsync(string refreshToken, string? ip, string? device)
        {
            var hashedToken = _tokenGenerator.HashToken(refreshToken);


            var storedToken = await _refreshTokenService.GetRefreshTokenAsync(hashedToken);

            if (storedToken == null)
                return Result<TokenResponse>.Failure("Invalid refresh token");

            if (storedToken.RevokedAt.HasValue)
                return Result<TokenResponse>.Failure("Refresh token revoked");

            if (storedToken.ExpiresAt < DateTime.UtcNow)
                return Result<TokenResponse>.Failure("Refresh token expired");


            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == storedToken.UserId);
            if (user == null)
                return Result<TokenResponse>.Failure("User not found");

            storedToken.RevokedAt = DateTime.UtcNow;
            storedToken.RevokedReason = "Replaced by new token";
            storedToken.RevokedByIp = ip;

            await _refreshTokenService.UpdateRefreshTokenAsync(storedToken);

            var permissions = await _permissionService.GetUserPermissionsAsync(user.Id);

            var tokenResponse = _tokenGenerator.GenerateTokenPair(user, permissions);

            await _refreshTokenService.SaveRefreshTokenAsync(
                user.Id,
                tokenResponse.RefreshToken,
                tokenResponse.RefreshTokenExpiration);

            return Result<TokenResponse>.Success(tokenResponse);
        }
        public async Task<Result> LogoutAsync(string refreshToken)
        {

            var hashedToken = _tokenGenerator.HashToken(refreshToken);

            var storedToken = await _refreshTokenService.GetRefreshTokenAsync(hashedToken);

            if (storedToken == null)
                return Result.Failure("Invalid refresh token");

            if (storedToken.RevokedAt.HasValue)
                return Result.Failure("Token already revoked");

            storedToken.RevokedAt = DateTime.UtcNow;
            storedToken.RevokedReason = "User logged out";

            await _refreshTokenService.UpdateRefreshTokenAsync(storedToken);

            return Result.Success();
        }
    }
}
