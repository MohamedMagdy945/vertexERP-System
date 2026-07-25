namespace VertexERP.Application.Shared.Constant;

public static class Roles
{
    public const string User = "user";
    public const string Admin = "admin";
    public const string SystemAdmin = "system_admin";
    public const string SecurityAdmin = "security_admin";

    public static IReadOnlyList<string> All() =>
    [
        Admin,
        User,
        SystemAdmin,
        SecurityAdmin,
    ];
}