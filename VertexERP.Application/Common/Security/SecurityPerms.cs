

using System.Reflection;
namespace VertexERP.Application.Common.Security;

public static class SecurityPerms
{
    public static class Identity
    {
        public const string View = "identity.view";
        public const string Manage = "identity.manage";
    }
    public static class Catalog
    {
        public const string View = "catalog.view";
        public const string Manage = "catalog.manage";
    }

    public static class Inventory
    {
        public const string View = "inventory.view";
        public const string Manage = "inventory.manage";
        public const string Adjustment = "inventory.adjustment.manage";
    }

    public static class Sales
    {
        public const string View = "sales.view";
        public const string Manage = "sales.manage";
    }

    public static class Purchases
    {
        public const string View = "purchases.view";
        public const string Manage = "purchases.manage";
    }

    public static class Finance
    {
        public const string View = "finance.view";
        public const string Manage = "finance.manage";
    }

    public static class Reports
    {
        public const string View = "reports.view";
        public const string Manage = "reports.manage";
    }

    public static class Settings
    {
        public const string View = "settings.view";
        public const string Manage = "settings.manage";
    }

    public static IReadOnlySet<string> All { get; } =
        typeof(SecurityPerms)
            .GetNestedTypes()
            .SelectMany(t => t.GetFields(
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
            .Select(f => (string)f.GetRawConstantValue()!)
            .ToHashSet();
}