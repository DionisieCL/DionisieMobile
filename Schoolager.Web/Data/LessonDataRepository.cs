using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public class LessonDataRepository : GenericRepository<LessonData>, ILessonDataRepository
    {
        private readonly DataContext _context;

        public LessonDataRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<StudentLessonData>> GetByLessonDatasAndStudentIdsAsync(int lessonDataId, List<int> studentIds)
        {
            return await _context.StudentLessonDatas
                .Where(sld => sld.LessonDataId == lessonDataId && studentIds.Contains(sld.StudentId))
                .ToListAsync();
        }

        public async Task<LessonResource> GetLessonResourceByLessonDataIdAsync(int lessonDataId)
        {
            return await _context.LessonResources
                .Where(lr => lr.LessonDataId == lessonDataId)
                .FirstOrDefaultAsync();
        }

        public async Task InsertLessonResourceAsync(LessonResource lessonResource)
        {
            _context.LessonResources.Add(lessonResource);

            await _context.SaveChangesAsync();
        }

        public async Task InsertStudentLessonDataRangeAsync(List<StudentLessonData> studentLessonDatas)
        {
            _context.StudentLessonDatas.AddRange(studentLessonDatas);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateLessonResourceAsync(LessonResource lessonResource)
        {
            _context.LessonResources.Update(lessonResource);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentLessonDataRangeAsync(List<StudentLessonData> studentLessonDatas)
        {
            _context.StudentLessonDatas.UpdateRange(studentLessonDatas);

            await _context.SaveChangesAsync();
        }
    }
}
