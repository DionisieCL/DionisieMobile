using System;
using System.Collections.Generic;

namespace Schoolager.Web.Data.Entities
{
    public class Lesson : IEntity
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string RecurrenceRule { get; set; }
        public string RecurrenceException { get; set; }
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }
        
        public ICollection<LessonData> LessonDatas { get; set; }
    }
}
