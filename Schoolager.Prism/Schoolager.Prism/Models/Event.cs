using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Schoolager.Prism.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public int RoomId { get; set; }

        public int WeekDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string RecurrenceRule { get; set; }
        public string RecurrenceException { get; set; }

        public int SubjectId { get; set; }

        public int TeacherId { get; set; }

        public int TurmaId { get; set; }

    }
}
