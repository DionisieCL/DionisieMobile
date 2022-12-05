using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<Teacher> GetTeachersBySubjectId(int subjectId)
        {
            return _context.Teachers
                .Where(t => t.SubjectId == subjectId);
        }

        public async Task<Teacher> GetWithUserByIdAsync(int id)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public IEnumerable<SelectListItem> GetComboTeachers()
        {
            var list = _context.Teachers.Select(s => new SelectListItem
            {
                Text = s.FullName,
                Value = s.Id.ToString(),
            }).OrderBy(sli => sli.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "< Select a teacher >",
                Value = "0",
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboTeachersBySubjectId(int subjectId)
        {
            var teachers = _context.Teachers.Where(t => t.SubjectId == subjectId).ToList();

            var list = teachers
                .Select(s => new SelectListItem
            {
                Text = s.FullName,
                Value = s.Id.ToString(),
            }).OrderBy(sli => sli.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "< Select a teacher >",
                Value = "0",
            });

            return list;
        }

        public async Task<Teacher> GetByUserIdAsync(string userId)
        {
            return await _context.Teachers
                .FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task<Teacher> GetByUserIdWithSubjectAsync(string userId)
        {
            return await _context.Teachers
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task<List<Teacher>> GetWithSubjectsAsync()
        {
            return await _context.Teachers
                .Include(t => t.Subject)
                .ToListAsync();
        }

        public async Task<Teacher> GetTeacherByIdWithSubjectAsync(int id)
        {
            return await _context.Teachers
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
