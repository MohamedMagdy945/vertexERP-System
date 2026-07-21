using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Inventory.Entities;

public sealed class Warehouse : Entity
{
    public string Name { get; private set; } = default!;

    public string Code { get; private set; } = default!;

    public string Location { get; private set; } = default!;

    public ICollection<Stock> Stocks { get; } = [];

    private Warehouse() { }

    public Warehouse(string name, string code, string location)
    {
        Name = name;
        Code = code;
        Location = location;
    }

    public void Update(string name, string code, string location)
    {
        Name = name;
        Code = code;
        Location = location;

        MarkAsUpdated();
    }
}