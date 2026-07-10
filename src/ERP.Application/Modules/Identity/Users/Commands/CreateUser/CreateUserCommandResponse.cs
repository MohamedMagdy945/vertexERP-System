namespace VertexERP.Application.Modules.Identity.Users.Commands.CreateUser;

public sealed record CreateUserCommandResponse(
    int Id,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    bool IsEnabled
    );
