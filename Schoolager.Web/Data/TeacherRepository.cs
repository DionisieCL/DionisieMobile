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

        public IQueryable<Teacher> GetTeachersBySubjectId(int subjectId)
        {
            return _context.Teachers
                .Where(t => t.SubjectId == subjectId);
        }

        public async Task<Teacher> GetTeacherWithSubject(int id)
        {
            return await _context.Teachers
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
