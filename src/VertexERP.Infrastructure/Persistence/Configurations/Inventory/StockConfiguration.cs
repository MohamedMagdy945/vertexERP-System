using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Infrastructure.Persistence.Configurations.Inventory;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.HasKey(x => new
        {
            x.ProductId,
            x.WarehouseId
        });

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Stocks)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Warehouse)
            .WithMany(x => x.Stocks)
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}

