using VertexERP.Domain.Common;

namespace VertexERP.Domain.Modules.Inventory.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
