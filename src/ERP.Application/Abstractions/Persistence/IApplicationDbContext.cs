using Microsoft.EntityFrameworkCore;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Abstractions.Persistence;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<RefreshToken> RefreshTokens { get; }
    public DbSet<Permission> Permissions { get; }
    public DbSet<UserPermission> UserPermissions { get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

