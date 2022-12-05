using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public class SubjectTurmaRepository : GenericRepository<SubjectTurma>, ISubjectTurmaRepository
    {
        private readonly DataContext _context;

        public SubjectTurmaRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Subject>> GetAllSubjectsWithTurma(int id)
        {
            return await _context.SubjectTurmas
                .Where(t => t.TurmaId == id)
                .Include(s => s.Subject)
                .Select(s => s.Subject)
                .ToListAsync();
        }

        public async Task InsertSubjectTurmasAsync(List<SubjectTurma> list)
        {
            _context.SubjectTurmas.AddRange(list);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSubjectTurmasAsync(List<SubjectTurma> list)
        {
            _context.SubjectTurmas.RemoveRange(list);
            await _context.SaveChangesAsync();
        }


    }
}
