using vertexERP.Domain.Common;

namespace vertexERP.Domain.Modules.HR
{
    public class Position : BaseEntity
    {
        public string Name { get; set; } = default!;
        public Money BaseSalary { get; set; } = default!;
    }
}
