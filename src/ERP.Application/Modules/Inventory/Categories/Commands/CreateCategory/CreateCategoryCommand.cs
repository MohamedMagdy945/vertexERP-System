using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Inventory.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(
    string Name,
    string Description
) : IRequest<Result<CreateCategoryCommandResponse>>;

