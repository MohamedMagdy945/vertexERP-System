namespace VertexERP.Application.Modules.Identity.Users.Commands.Create;

public sealed record Response(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);