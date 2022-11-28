using System;
using System.Collections.Generic;

namespace Schoolager.Web.Data.Entities
{
    public class LessonData : IEntity
    {
        public int Id { get; set; }

        public string Summary { get; set; }

        public DateTime LessonDate { get; set; }

        public ICollection<StudentLessonData> StudentLessonsData { get; set; }

        public Lesson Lesson { get; set; }

        public int LessonId { get; set; }
    }
}
