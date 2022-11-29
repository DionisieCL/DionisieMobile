using Microsoft.AspNetCore.Mvc.Rendering;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;

namespace Schoolager.Web.Data
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        IEnumerable<SelectListItem> GetComboSubjects();
    }
}