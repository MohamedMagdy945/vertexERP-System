namespace vertexERP.Domain.Modules.HR
{
    public class Employee
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string JobTitel { get; set; } = default!;
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public int BranchId { get; set; }
        public int? ManagerId { get; set; }
        public int PositionId { get; set; }
        public string UserId { get; set; } = default!;

        public Department Department { get; set; } = null!;
        public Position Position { get; set; } = null!;
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
