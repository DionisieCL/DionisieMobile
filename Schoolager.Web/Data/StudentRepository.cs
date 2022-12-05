using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {

        private readonly DataContext _context;

        public StudentRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetStudentWithTurma(int id)
        {
            return await _context.Students
                .Where(s => s.TurmaId == id)
                .Include(s => s.Turma)
                .ToListAsync();
        }

    }
}
