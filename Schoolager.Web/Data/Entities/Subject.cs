using System.Collections.Generic;

namespace Schoolager.Web.Data.Entities
{
    public class Subject : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Turma Turma { get; set; }
        public int TurmaId { get; set; }

        public ICollection<Teacher> Teachers { get; set; }

        public ICollection<Grade> Grades { get; set; }
    }
}
