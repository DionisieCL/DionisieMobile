using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public interface ISubjectTurmaRepository : IGenericRepository<SubjectTurma>
    {
        Task<List<Subject>> GetAllSubjectsWithTurma(int id);
        Task InsertSubjectTurmasAsync(List<SubjectTurma> list);
        Task RemoveSubjectTurmasAsync(List<SubjectTurma> list);
    }
}
