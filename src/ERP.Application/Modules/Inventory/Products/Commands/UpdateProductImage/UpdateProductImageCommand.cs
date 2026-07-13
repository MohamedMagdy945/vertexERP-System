using MediatR;
using Microsoft.AspNetCore.Http;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Products.Commands.UpdateProductImage;

public record UpdateProductImageCommand(
    int Id,
    IFormFile Image
) : IRequest<Result<UpdateProductImageCommandResponse>>;