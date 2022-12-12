using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public interface ILessonDataRepository : IGenericRepository<LessonData>
    {
        Task<List<StudentLessonData>> GetByLessonDatasAndStudentIdsAsync(int lessonDataId, List<int> studentIds);
        Task UpdateStudentLessonDataRangeAsync(List<StudentLessonData> studentLessonDatas);
        Task InsertStudentLessonDataRangeAsync(List<StudentLessonData> studentLessonDatas);
        Task<LessonResource> GetLessonResourceByLessonDataIdAsync(int lessonDataId);
        Task InsertLessonResourceAsync(LessonResource lessonResource);
        Task UpdateLessonResourceAsync(LessonResource lessonResource);
        Task<List<Doubt>> GetDoubtsByLessonDataIdAsync(int id);
        Task<Doubt> GetDoubtByIdAsync(int id);
        Task UpdateDoubtAsync(Doubt doubt);
        Task<List<Doubt>> GetDoubtsByLessonDataAndStudentIdsAsync(int lessonDataId, int studentId);
        Task InsertDoubt(Doubt doubt);
    }
}
