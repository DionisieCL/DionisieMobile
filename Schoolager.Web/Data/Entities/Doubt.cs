namespace Schoolager.Web.Data.Entities
{
    public class Doubt : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Answer { get; set; }

        public Student Student { get; set; }

        public int StudentId { get; set; }

        public LessonData LessonData { get; set; }

        public int LessonDataId { get; set; }
    }
}
