using vertexERP.Domain.Common;

namespace vertexERP.Domain.Modules.HR
{
    public class Position : BaseEntity
    {
        public string Name { get; set; } = default!;
        public decimal BaseSalary { get; set; } = default!;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
