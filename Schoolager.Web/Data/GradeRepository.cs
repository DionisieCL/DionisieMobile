using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        private readonly DataContext _context;

        public GradeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Grade>> GetGradesWithStudent(int id)
        {
            return await _context.Grades
                .Where(g => g.StudentId == id)
                .Include(s => s.Subject)
                .Include(s => s.Student)
                .ToListAsync();
        }

        public async Task InsertGradesAsync(List<Grade> grades)
        {
            _context.Grades.AddRange(grades);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateGradesAsync(List<Grade> grades)
        {
            _context.Grades.UpdateRange(grades);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Grade>> GetGradesBySubjectAndStudentIdsAsync(int subjectId, List<int> studentIds)
        {
            return await _context.Grades
                .Where(g => g.SubjectId == subjectId && studentIds.Contains(g.StudentId))
                .ToListAsync();
        }
    }
}
