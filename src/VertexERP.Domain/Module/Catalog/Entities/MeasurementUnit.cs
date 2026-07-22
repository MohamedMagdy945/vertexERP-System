using VertexERP.Domain.Common;

namespace VertexERP.Domain.Module.Catalog.Entities;

public sealed class MeasurementUnit : Entity
{
    public string Symbol { get; private set; } = default!;
    private MeasurementUnit() { }

    public MeasurementUnit(string symbol)
    {
        Symbol = FormatSymbol(symbol);
    }
    public void Update(string symbol)
    {
        Symbol = FormatSymbol(symbol);
    }
    public static string FormatSymbol(string symbol)
    {
        return symbol.Trim().ToUpperInvariant();
    }
}