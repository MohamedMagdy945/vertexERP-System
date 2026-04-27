using VertexERP.Domain.Common;

namespace VertexERP.Domain.Modules.HR
{
    public class Position : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal BaseSalary { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
