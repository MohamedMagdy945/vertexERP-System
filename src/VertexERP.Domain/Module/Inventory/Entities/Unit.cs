using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Inventory.Entities;

public sealed class Unit : Entity
{
    public string Name { get; private set; } = default!;

    public string Symbol { get; private set; } = default!;

    private Unit() { }

    public Unit(string name, string symbol)
    {
        Name = name;
        Symbol = symbol;
    }
}