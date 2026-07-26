using VertexERP.Domain.Module.Identity.Entities;

namespace VertexERP.Application.Modules.Identity.Users.Roles.Delete;

public static class Projection
{
    public static IQueryable<RoleResponse> ToResponse(this IQueryable<User> query)
    {
        return query.SelectMany(user => user.UserRoles)
                .Select(userRole => new RoleResponse
                {
                    Id = userRole.Role.Id,
                    Name = userRole.Role.Name
                });
    }
};