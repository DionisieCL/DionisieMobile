namespace Schoolager.Web.Data.Entities
{
    public class Room : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Lesson Lesson { get; set; }
    }
}
