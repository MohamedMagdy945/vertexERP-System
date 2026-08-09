namespace VertexERP.Application.Common.Security;

public static class SecurityRoles
{
    public const string StandardUser = "standard_user";
    public const string SystemAdmin = "system_admin";
    public const string SecurityAdmin = "security_admin";

    public static IReadOnlyCollection<string> All { get; } =
    [
        StandardUser,
        SystemAdmin,
        SecurityAdmin
    ];
}

