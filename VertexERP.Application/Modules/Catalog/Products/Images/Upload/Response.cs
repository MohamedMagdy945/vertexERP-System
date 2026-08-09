namespace VertexERP.Application.Modules.Catalog.Products.Images.Upload;

public sealed record Response(Guid ProductId, IReadOnlyList<ImageResponse> Images);
public sealed record ImageResponse(Guid Id, string Url);