using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public interface ITurmaRepository : IGenericRepository<Turma>
    {
        Task<List<Turma>> GetTurmasByTeacherIdAsync(int id);
        Turma GetWithStudentsById(int id);
        Task<List<Subject>> GetTurmaSubjects(int id);

    }
}