namespace VertexERP.Application.Services.Update;

public sealed record Response(
    Guid Id,
    string Name,
    string? Description,
    string? ImageUrl);