using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Inventory.Entities;

public sealed class ProductImage : Entity
{
    public string Url { get; private set; } = default!;
    public string? AltText { get; private set; }
    public bool IsPrimary { get; private set; }
    public int ProductId { get; private set; }
    public Product Product { get; private set; } = default!;
    private ProductImage() { }

    public ProductImage(string url, int productId, bool isPrimary = false, string? altText = null)
    {
        Url = url;
        ProductId = productId;
        IsPrimary = isPrimary;
        AltText = altText;
    }

    public void Update(string url, string? altText)
    {
        Url = url;
        AltText = altText;

        MarkAsUpdated();
    }

    public void SetAsPrimary()
    {
        IsPrimary = true;

        MarkAsUpdated();
    }

    public void RemoveAsPrimary()
    {
        IsPrimary = false;

        MarkAsUpdated();
    }
}