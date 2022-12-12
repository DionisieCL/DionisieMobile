using Schoolager.Web.Data.Entities;
using System.Collections.Generic;

namespace Schoolager.Web.Models.Lessons
{
    public class LessonDataViewModel
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string DateString { get; set; }
        public string SubjectName { get; set; }
        public string Summary { get; set; }
        public string Homework { get; set; }
        public List<AttendanceViewModel> Attendances { get; set; }
        public List<DoubtViewModel> Doubts { get; set; }
        public LessonResourceViewModel LessonResource { get; set; }
    }
}
