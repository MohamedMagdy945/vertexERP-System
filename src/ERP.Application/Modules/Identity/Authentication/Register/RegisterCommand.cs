using MediatR;
using VertexERP.Shared.Results;

namespace VertexERP.Application.Modules.Identity.Authentication.Register;

public record RegisterCommand(string FullName, string Email, string Password, string ConfirmPassword, string PhoneNumber)
    : IRequest<Result<RegisterResponse>>;

