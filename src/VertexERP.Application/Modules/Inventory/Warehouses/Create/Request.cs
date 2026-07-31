using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Modules.Inventory.Warehouses.Create;

public sealed record Request(
    string Name,
    string? Description,
    IFormFile? Image);
