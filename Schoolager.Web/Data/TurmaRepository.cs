using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public class TurmaRepository : GenericRepository<Turma>, ITurmaRepository
    {
        private readonly DataContext _context;

        public TurmaRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Turma>> GetTurmasByTeacherIdAsync(int id)
        {
            return _context.Turmas
                .Where(t => t.TeacherTurmas.Any(tt => tt.TeacherId == id))
                .ToListAsync();
        }

        public Turma GetWithStudentsById(int id)
        {
            return _context.Turmas
                .Where(t => t.Id == id)
                .Include(t => t.Students)
                .FirstOrDefault();
        }
    }
}
