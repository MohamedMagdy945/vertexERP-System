namespace VertexERP.Application.Services.Get;

public sealed class Response
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public string? Description { get; init; }
}