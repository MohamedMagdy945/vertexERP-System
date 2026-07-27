using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Modules.Catalog.Categories.Create;

public sealed record Request(
    string Name,
    string? Description,
    IFormFile? Image);
