namespace Schoolager.Web.Data.Entities
{
    public class TeacherTurma : IEntity
    {
        public int Id { get; set; }

        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }

        public Turma Turma { get; set; }
        public int TurmaId { get; set; }
    }
}
