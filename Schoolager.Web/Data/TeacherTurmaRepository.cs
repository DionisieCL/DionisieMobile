using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public class TeacherTurmaRepository : GenericRepository<TeacherTurma>, ITeacherTurmaRepository
    {
        private readonly DataContext _context;

        public TeacherTurmaRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Teacher GetTeacherWithSubjectInTurma(int turmaId, int subjectId)
        {
            return _context.TeacherTurmas
                .Where(t => t.TurmaId == turmaId)
                .Select(te => te.Teacher)
                .Where(te => te.SubjectId == subjectId)
                .FirstOrDefault();
        }

        public async Task<List<TeacherTurma>> GetRecordByTeacherAndTurmaAsync(int turmaId, List<int> teacherId)
        {
            return await _context.TeacherTurmas
                .Where(t => t.TurmaId == turmaId && teacherId.Contains(t.TeacherId))
                .ToListAsync();
        }

        public async Task InsertTeacherTurmaAsync(List<TeacherTurma> teacherTurma)
        {
            _context.TeacherTurmas.AddRange(teacherTurma);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeacherTurmaAsync(List<TeacherTurma> teacherTurma)
        {
            _context.TeacherTurmas.UpdateRange(teacherTurma);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeacherTurmaAsync(List<TeacherTurma> teacherTurma)
        {
            _context.TeacherTurmas.RemoveRange(teacherTurma);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllByTurmaId(int turmaId)
        {
            var teacherTurmas = await _context.TeacherTurmas
                .Where(tt => tt.TurmaId == turmaId)
                .ToListAsync();

            _context.TeacherTurmas.RemoveRange(teacherTurmas);

            await _context.SaveChangesAsync();
        }

        public async Task<Teacher> GetTeacherBySubjectAndTurmaIdAsync(int subjectId, int turmaId)
        {
            return await _context.TeacherTurmas
                .Where(tt => tt.TurmaId == turmaId && tt.SubjectId == subjectId)
                .Select(tt => tt.Teacher)
                .FirstOrDefaultAsync();
        }
    }
}
