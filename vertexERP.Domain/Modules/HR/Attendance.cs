using VertexERP.Domain.Common;

namespace VertexERP.Domain.Modules.HR
{
    public class Attendance : BaseEntity
    {
        public int EmployeeId { get; set; }
        public AttendanceType AttendanceType { get; set; }

        public Employee Employee { get; set; } = new Employee();
    }
}
