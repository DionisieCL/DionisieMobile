using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        private readonly DataContext _context;

        public LessonRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Lesson> GetAllWithMembers()
        {
            return _context.Lessons
                .Include(l => l.Subject)
                .Include(l => l.Teacher);
        }

        public Task<Lesson> GetLessonByIdAsync(int id)
        {
            return _context.Lessons
                .Include(l => l.Subject)
                .Include(l => l.Teacher)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}
