namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.GetById;

public sealed class Response
{
    public Guid Id { get; init; }
    public string Symbol { get; init; } = default!;
}