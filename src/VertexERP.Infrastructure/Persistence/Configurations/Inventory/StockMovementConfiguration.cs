using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Infrastructure.Persistence.Configurations.Inventory;

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.Property(x => x.MovementType)
                .HasConversion<int>()
                .IsRequired();

        builder.HasOne(x => x.Product)
           .WithMany()
           .HasForeignKey(x => x.ProductId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Warehouse)
        .WithMany()
        .HasForeignKey(x => x.WarehouseId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.IssuedByUser)
         .WithMany()
         .HasForeignKey(x => x.IssuedByUserId)
         .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ReceivedByUser)
          .WithMany()
          .HasForeignKey(x => x.ReceivedByUserId)
          .OnDelete(DeleteBehavior.Restrict);
    }
}

