using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Services.Update;

public sealed record Request(
    Guid Id,
    string Name,
    string? Description,
    IFormFile? Image);