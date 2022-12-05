using Microsoft.AspNetCore.Mvc.Rendering;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        Task<Teacher> GetWithUserByIdAsync(int id);
        IQueryable<Teacher> GetTeachersBySubjectId(int subjectId);
        IEnumerable<SelectListItem> GetComboTeachers();
        IEnumerable<SelectListItem> GetComboTeachersBySubjectId(int subjectId);
        Task<Teacher> GetByUserIdAsync(string userId);
        Task<Teacher> GetByUserIdWithSubjectAsync(string userId);
        Task<List<Teacher>> GetWithSubjectsAsync();
        Task<Teacher> GetTeacherByIdWithSubjectAsync(int id);
    }
}