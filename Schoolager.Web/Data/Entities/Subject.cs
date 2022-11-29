using System.Collections.Generic;

namespace Schoolager.Web.Data.Entities
{
    public class Subject : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SubjectTurma> SubjectTurma { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}
