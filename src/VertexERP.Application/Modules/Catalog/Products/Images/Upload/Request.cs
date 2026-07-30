using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Modules.Catalog.Products.Images.Upload;

public sealed record Request(
    Guid ProductId,
    IReadOnlyList<IFormFile> Images);