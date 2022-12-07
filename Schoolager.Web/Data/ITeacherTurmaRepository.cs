using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public interface ITeacherTurmaRepository: IGenericRepository<TeacherTurma>
    {
        Teacher GetTeacherWithSubjectInTurma(int turmaId, int subjectId);
        Task<List<TeacherTurma>> GetRecordByTeacherAndTurmaAsync(int turmaId, List<int> teacherId);
        Task InsertTeacherTurmaAsync(List<TeacherTurma> teacherTurma);
        Task UpdateTeacherTurmaAsync(List<TeacherTurma> teacherTurma);
        Task DeleteTeacherTurmaAsync(List<TeacherTurma> teacherTurma);

    }
}
