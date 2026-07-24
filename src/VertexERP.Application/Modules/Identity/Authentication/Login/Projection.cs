using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Authentication.Login;

public static class Projection
{
    public static IQueryable<Context> ToContext(this IQueryable<User> query)
    {
        return query.Select(user => new Context(user.Id, user.Email, user.PasswordHash, user.IsActive, user.UserRoles
                     .Select(userRole => userRole.Role.Name).ToList()));
    }
}