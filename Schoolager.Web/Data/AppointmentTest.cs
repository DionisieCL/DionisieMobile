using Schoolager.Web.Data.Entities;
using System;

namespace Schoolager.Web.Data
{
    public class AppointmentTest
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public Subject Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string RecurrenceRule { get; set; }
    }
}
