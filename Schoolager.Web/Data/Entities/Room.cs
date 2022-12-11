using System.Collections.Generic;

namespace Schoolager.Web.Data.Entities
{
    public class Room : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}
