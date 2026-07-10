using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword,
    List<int> PermissionIds,
    bool IsEnabled
) : IRequest<Result<CreateUserCommandResponse>>;

