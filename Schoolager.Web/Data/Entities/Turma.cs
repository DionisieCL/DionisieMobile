using System.Collections.Generic;

namespace Schoolager.Web.Data.Entities
{
    public class Turma : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }


        public ICollection<SubjectTurma> SubjectTurma;
    }
}
