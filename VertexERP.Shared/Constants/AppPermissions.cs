namespace VertexERP.Shared.Constants
{
    public static class AppPermissions
    {
        public const string Create = "Permissions.Products.Create";
        public const string View = "Permissions.Products.View";
        public const string Edit = "Permissions.Products.Edit";
        public const string Delete = "Permissions.Products.Delete";

        public static List<string> GetAll() => new() { Create, View, Edit, Delete };
    }
}
