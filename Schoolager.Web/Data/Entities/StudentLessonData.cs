namespace Schoolager.Web.Data.Entities
{
    public class StudentLessonData : IEntity
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public LessonData LessonData { get; set; }
        public int LessonDataId { get; set; }
        public bool WasPresent { get; set; }
    }
}
