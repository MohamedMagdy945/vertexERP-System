using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Catalog.Entities;

public sealed class Category : Entity
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public string? ImageUrl { get; private set; }
    public ICollection<Product> Products { get; } = [];

    private Category() { }

    public Category(string name, string? description, string? imageUrl)
    {
        Name = FormatName(name);
        Description = description;
        ImageUrl = imageUrl;
    }

    public void Update(string name, string? description, string? imageUrl = null)
    {
        Name = FormatName(name);
        Description = description;

        if (imageUrl is not null)
            ImageUrl = imageUrl;

        MarkAsUpdated();
    }
    public static string FormatName(string name)
    {
        name = name.Trim().ToLowerInvariant();

        return char.ToUpperInvariant(name[0]) + name[1..];
    }
}