using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Models.Lessons;
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

        public async Task<Lesson> CheckTeacherAvailabilityAsync(Lesson lesson)
        {
            return await _context.Lessons
                .Where(l => l.TeacherId == lesson.TeacherId
                && l.WeekDay == lesson.WeekDay
                && (l.StartTime.Value.TimeOfDay < lesson.EndTime.Value.TimeOfDay
                    && lesson.StartTime.Value.TimeOfDay < l.EndTime.Value.TimeOfDay))
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync();
        }

        public async Task<Lesson> CheckRoomAvailabilityAsync(Lesson lesson)
        {
            return await _context.Lessons
                .Where(l => l.RoomId == lesson.RoomId
                && l.WeekDay == lesson.WeekDay
                && (l.StartTime.Value.TimeOfDay < lesson.EndTime.Value.TimeOfDay
                    && lesson.StartTime.Value.TimeOfDay < l.EndTime.Value.TimeOfDay))
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync();
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

        public async Task<List<Lesson>> GetLessonByStudentIdAsync(int id)
        {
            return await _context.Lessons
                .Where(l => l.Turma.Students.Any(s => s.Id == id))
                .ToListAsync();
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

        public async Task<SchoolYear> GetSchoolYearAsync()
        {
            return await _context.SchoolYears
                .FirstOrDefaultAsync();
        }

        public async Task UpdateSchoolYearAsync(SchoolYear schoolYear)
        {
            _context.SchoolYears.Update(schoolYear);

            await _context.SaveChangesAsync();
        }
    }
}
