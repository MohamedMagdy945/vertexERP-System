using Mapster;
using VertexERP.Application.Modules.Inventory.Products.Commands.UpdateProduct;
using VertexERP.Domain.Module.Inventory.Entities;

namespace VertexERP.Application.Common.Mapping;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateProductCommand, Product>()
              .IgnoreNullValues(true);
    }
}

