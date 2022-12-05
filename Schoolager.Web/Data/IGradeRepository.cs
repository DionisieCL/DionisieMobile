using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public interface IGradeRepository : IGenericRepository<Grade>
    {
        Task<List<Grade>> GetGradesWithStudent(int id);
        Task InsertGradesAsync(List<Grade> grades);

        Task UpdateGradesAsync(List<Grade> grades);
        Task<List<Grade>> GetGradesBySubjectAndStudentIdsAsync(int subjectId, List<int> studentIds);

    }
}