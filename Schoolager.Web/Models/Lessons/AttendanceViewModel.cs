using Schoolager.Web.Models.Students;

namespace Schoolager.Web.Models.Lessons
{
    public class AttendanceViewModel
    {
        public StudentViewModel StudentViewModel { get; set; }
        public int StudentId { get; set; }
        public bool WasPresent { get; set; }
    }
}
