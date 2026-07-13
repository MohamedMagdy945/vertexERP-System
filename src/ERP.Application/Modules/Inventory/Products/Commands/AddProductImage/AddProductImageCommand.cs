using MediatR;
using Microsoft.AspNetCore.Http;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.AddProductImage;

public record AddProductImageCommand(
     int ProductId,
    IFormFile Image
) : IRequest<Result<AddProductImageCommandResponse>>;