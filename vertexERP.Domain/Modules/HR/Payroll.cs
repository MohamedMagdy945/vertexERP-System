namespace vertexERP.Domain.Modules.HR
{
    public class Payroll
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Allowances { get; set; }

        public decimal Deductions { get; set; }

        public decimal NetSalary => BaseSalary + Allowances - Deductions;

        public DateTime PaidDate { get; set; }
    }
}
