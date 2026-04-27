namespace VertexERP.Domain.Modules.HR
{
    public class Employee
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobTitel { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public int BranchId { get; set; }
        public int? ManagerId { get; set; }
        public int PositionId { get; set; }
        public int UserId { get; set; }

        public Department Department { get; set; } = default!;
        public Position Position { get; set; } = null!;
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
