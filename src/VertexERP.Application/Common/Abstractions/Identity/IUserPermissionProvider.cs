namespace VertexERP.Application.Common.Abstractions.Identity;

public interface IUserPermissionProvider
{
    IQueryable<string> Get(Guid userId);
}