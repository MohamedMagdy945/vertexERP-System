namespace VertexERP.Application.Common.Authorization
{
    public static class AppRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public static List<string> GetAll() => new() { Admin, User };

    }
}
