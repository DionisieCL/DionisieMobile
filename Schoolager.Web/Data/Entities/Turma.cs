using System.Collections.Generic;

namespace Schoolager.Web.Data.Entities
{
    public class Turma : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        //Appointment
        public ICollection<Student> Students { get; set; }

        public ICollection<SubjectTurma> SubjectTurmas;
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<TeacherTurma> TeacherTurmas{ get; set; }
    }
}
