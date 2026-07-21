using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Catalog.Entities;

public sealed class MeasurementUnit : Entity
{
    public string Name { get; private set; } = default!;

    public string Symbol { get; private set; } = default!;

    private MeasurementUnit() { }

    public MeasurementUnit(string name, string symbol)
    {
        Name = name;
        Symbol = symbol;
    }
}