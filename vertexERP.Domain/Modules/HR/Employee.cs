using vertexERP.Domain.Common;

namespace vertexERP.Domain.Modules.HR
{
    public class Employee
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string JobTitel { get; set; } = default!;
        public DateTime HireDate { get; set; }
        public Money Salary { get; set; }
        public int DepartmentId { get; set; }
        public int BranchId { get; set; }
        public int? ManagerId { get; set; }
        public string UserId { get; set; } = default!;

    }
}
