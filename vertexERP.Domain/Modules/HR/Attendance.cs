using vertexERP.Domain.Common;

namespace vertexERP.Domain.Modules.HR
{
    public class Attendance : BaseEntity
    {
        public int EmployeeId { get; set; }
        public AttendanceType AttendanceType { get; set; }

        public Employee Employee { get; set; } = new Employee();
    }
}
