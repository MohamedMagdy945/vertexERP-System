using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VertexERP.Application.Shared.Constant;
using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Infrastructure.Persistence.Configurations.Identity;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", Schemas.Identity);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();


        builder.Property(x => x.Email)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.Portal)
             .HasConversion<string>();

        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}

