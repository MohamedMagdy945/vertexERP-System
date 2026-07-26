using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VertexERP.Application.Shared.Constant;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Infrastructure.Persistence.Configurations.Identity;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions", Schemas.Identity);

        builder.HasKey(x => new
        {
            x.RoleId,
            x.Permission
        });

        builder.Property(x => x.Permission)
            .HasMaxLength(25)
            .IsRequired();

        builder.HasOne(x => x.Role)
            .WithMany(x => x.RolePermissions)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
