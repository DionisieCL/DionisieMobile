using Schoolager.Web.Data.Entities;

namespace Schoolager.Web.Data
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        private readonly DataContext _context;

        public LessonRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
