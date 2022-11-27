namespace Schoolager.Web.Data.Entities
{
    public class SubjectTurma : IEntity
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
    }
}
