using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Catalog.Entities;

public sealed class Category : Entity
{
    public string Name { get; private set; } = default!;

    public string? Description { get; private set; }

    public ICollection<Product> Products { get; } = [];

    private Category() { }

    public Category(string name, string? description = null)
    {
        Name = FormatName(name);
        Description = description;
    }

    public void Update(string name, string? description)
    {
        Name = FormatName(name);
        Description = description;

        MarkAsUpdated();
    }
    public static string FormatName(string name)
    {
        name = name.Trim().ToLowerInvariant();

        return char.ToUpperInvariant(name[0]) + name[1..];
    }
}