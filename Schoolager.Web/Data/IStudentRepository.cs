using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<List<Student>> GetStudentWithTurma(int id);
        List<Student> GetFreeStudents();
        Task UpdateRangeAsync(List<Student> students);
        List<Student> GetByTurmaId(int id);
    }
}
