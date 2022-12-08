using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using System;
using System.Collections.Generic;
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

        public async Task<List<Lesson>> GetLessonByTeacherIdAsync(int id)
        {
            return await _context.Lessons
                .Where(l => l.TeacherId == id)
                .ToListAsync();
        }

        public async Task<List<Lesson>> GetLessonByTurmaIdAsync(int id)
        {
            return await _context.Lessons
                .Where(l => l.TurmaId == id)
                .ToListAsync();
        }

        public async Task<LessonData> GetLessonData(int lessonId, DateTime lessonDate)
        {
            return await _context.LessonDatas
                .Where(ld => ld.LessonId == lessonId && ld.LessonDate == lessonDate)
                .FirstOrDefaultAsync();
        }

    }
}
