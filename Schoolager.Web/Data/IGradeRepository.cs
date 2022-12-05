using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public interface IGradeRepository : IGenericRepository<Grade>
    {
        Task<List<Grade>> GetGradesWithStudent(int id);
    }
}