using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {

        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public StudentRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
        }

        public List<Student> GetByTurmaId(int id)
        {
            return _context.Students
                .Where(s => s.TurmaId == id)
                .AsNoTrackingWithIdentityResolution()
                .ToList();
        }

        public async Task<Student> GetByUserIdAsync(string userId)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public List<Student> GetFreeStudents()
        {
            return _context.Students
                .Where(s => s.TurmaId == null)
                .AsNoTrackingWithIdentityResolution()
                .ToList();
        }

        public List<Student> GetFreeStudentsBySchoolYear(int schoolYear)
        {
            return _context.Students
                .Where(s => s.SchoolYear == schoolYear && s.TurmaId == null)
                .AsNoTrackingWithIdentityResolution()
                .ToList();
        }

        //public async Task<Student> GetStudentWithTurma(int id)
        //{
        //    return await _context.Students
        //        .Include(s => s.Turma)
        //        .Where(s => s.TurmaId == id)
        //        .FirstOrDefaultAsync();
        //}

        public async Task<List<Student>> GetStudentWithTurma(int id)
        {
            return await _context.Students
                .Where(s => s.TurmaId == id)
                .Include(s => s.Turma)
                .ToListAsync();
        }

        public async Task UpdateRangeAsync(List<Student> students)
        {
            _context.UpdateRange(students);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetStudentGradesWithUser(int id, string email)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);

            return await _context.Students
                .Where(s => s.TurmaId == id && s.UserId == user.Id)
                .Include(s => s.Turma)
                .ToListAsync();
        }

    }
}
