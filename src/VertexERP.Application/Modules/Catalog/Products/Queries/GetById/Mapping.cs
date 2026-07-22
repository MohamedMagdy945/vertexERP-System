
using Mapster;
using VertexERP.Domain.Module.Catalog.Entities;


namespace VertexERP.Application.Modules.Catalog.Products.Queries.GetById;

public sealed class Mapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, Response>()
            .Map(dest => dest.Images,
                src => src.Images.Select(x => x.Url).ToList());
    }
}