namespace VertexERP.Shared.Constant;

public static class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string System = "System";

    public static IReadOnlyList<string> GetAll() =>
    [
        Admin,
        User,
        System
    ];
}