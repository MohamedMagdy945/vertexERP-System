using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Modules.Catalog.Categories.Update;

public sealed record Request(
    Guid Id,
    string Name,
    string? Description,
    IFormFile? Image);