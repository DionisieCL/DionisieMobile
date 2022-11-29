using Schoolager.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Schoolager.Web.Models.Lessons
{
    public class LessonViewModel : Lesson
    {
        public string StartTimeString { get; set; }

        public string EndTimeString { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must select a day")]
        public int WeekDay { get; set; }
    }
}
