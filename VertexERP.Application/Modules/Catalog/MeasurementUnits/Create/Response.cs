namespace VertexERP.Application.Modules.Catalog.MeasurementUnits.Create;

public sealed class Response
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string? Description { get; init; }
}