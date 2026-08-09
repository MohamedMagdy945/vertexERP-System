using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VertexERP.Domain.Module.Catalog.Entities;

namespace VertexERP.Infrastructure.Persistence.Configurations.Catalog;

public sealed class MeasurementUnitConfiguration : IEntityTypeConfiguration<MeasurementUnit>
{
    public void Configure(EntityTypeBuilder<MeasurementUnit> builder)
    {
        builder.ToTable("Units");

        builder.HasKey(x => x.Id);


        builder.Property(x => x.Symbol)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(x => x.Symbol)
            .IsUnique();
    }
}