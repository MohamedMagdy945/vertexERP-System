using Microsoft.AspNetCore.Http;

namespace VertexERP.Application.Modules.Catalog.Products.Images.Upload;

public sealed record Request(
    IReadOnlyList<IFormFile> Images);