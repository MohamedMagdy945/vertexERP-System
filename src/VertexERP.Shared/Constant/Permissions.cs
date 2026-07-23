namespace VertexERP.Shared.Constant;

public static class Permissions
{
    public static class Orders
    {
        public const string Read = "orders.read";
        public const string Create = "orders.create";
        public const string Update = "orders.update";
        public const string Delete = "orders.delete";
    }

    public static class Purchases
    {
        public const string Read = "purchases.read";
        public const string Create = "purchases.create";
        public const string Update = "purchases.update";
        public const string Delete = "purchases.delete";
    }

    public static class Inventory
    {
        public const string Read = "inventory.read";
        public const string Create = "inventory.create";
        public const string Update = "inventory.update";
        public const string Delete = "inventory.delete";
    }

    public static class Hr
    {
        public const string Read = "hr:read";
        public const string Create = "hr:create";
        public const string Update = "hr:update";
        public const string Delete = "hr:delete";
    }
}