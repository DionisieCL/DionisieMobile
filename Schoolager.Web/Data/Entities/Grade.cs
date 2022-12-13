namespace Schoolager.Web.Data.Entities
{
    public class Grade : IEntity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public double FirstMark { get; set; }
        public double SecondMark { get; set; }
        public double ThirdMark { get; set; }
    }
}
