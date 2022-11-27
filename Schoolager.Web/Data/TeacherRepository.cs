using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        private readonly DataContext _context;

        public TeacherRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Teacher> GetTeacherWithSubject(int id)
        {
            return await _context.Teachers
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
