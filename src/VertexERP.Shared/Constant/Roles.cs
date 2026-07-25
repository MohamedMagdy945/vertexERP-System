namespace VertexERP.Shared.Constant;

public static class Roles
{
    public const string User = "User";
    public const string Admin = "Admin";
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