using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Infrastructure.Persistence.Configurations.Inventory;

public sealed class WarehouseTransactionConfiguration
    : IEntityTypeConfiguration<WarehouseTransaction>
{
    public void Configure(EntityTypeBuilder<WarehouseTransaction> builder)
    {
        builder.ToTable("WarehouseTransactions");

        builder.HasKey(x => x.Id);


        builder.Property(x => x.Quantity)
            .IsRequired();


        builder.Property(x => x.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.ReferenceType)
            .HasConversion<int>()
            .IsRequired();


        builder.Property(x => x.TransactionDate)
            .IsRequired();


        builder.Property(x => x.ReferenceNumber)
            .HasMaxLength(100);

        builder.Property(x => x.Notes)
            .HasMaxLength(500);


        builder.HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(x => x.PerformedByUser)
            .WithMany()
            .HasForeignKey(x => x.PerformedByUserId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(x => new
        {
            x.WarehouseId,
            x.ProductId,
            x.TransactionDate
        });
    }
}