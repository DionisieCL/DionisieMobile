using Schoolager.Web.Data.Entities;
using System.Collections.Generic;

namespace Schoolager.Web.Models.Lessons
{
    public class LessonDataViewModel
    {
        public string DateString { get; set; }
        public string SubjectName { get; set; }
        public string Summary { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
