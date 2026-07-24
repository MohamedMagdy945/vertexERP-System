using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VertexERP.Domain.Module.Identity.Entities;
using VertexERP.Shared.Constant;

namespace VertexERP.Infrastructure.Persistence.Configurations.Identity;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {

        builder.ToTable("Roles", Schemas.Identity);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(x => x.Name)
          .IsUnique();

        builder.Property(x => x.Description)
          .HasMaxLength(100);
    }
}

