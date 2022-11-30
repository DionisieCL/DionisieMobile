using Microsoft.AspNetCore.Mvc.Rendering;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        Task<Teacher> GetTeacherWithSubject(int id);
        IQueryable<Teacher> GetTeachersBySubjectId(int subjectId);
        IEnumerable<SelectListItem> GetComboTeachers();
        IEnumerable<SelectListItem> GetComboTeachersBySubjectId(int subjectId);
    }
}