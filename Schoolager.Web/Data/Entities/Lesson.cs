using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schoolager.Web.Data.Entities
{
    public class Lesson : IEntity
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string Location { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string RecurrenceRule { get; set; }
        public string RecurrenceException { get; set; }
        public Subject Subject { get; set; }

        [Range(1, int.MaxValue, ErrorMessage="You must select a Subject")]
        public int SubjectId { get; set; }
        public Teacher Teacher { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must select a Teacher")]
        public int TeacherId { get; set; }
        public ICollection<LessonData> LessonDatas { get; set; }
    }
}
