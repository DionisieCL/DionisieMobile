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

        public async Task<Doubt> GetDoubtByIdAsync(int id)
        {
            return await _context.Doubts
                .Include(d => d.Student)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Doubt>> GetDoubtsByLessonDataAndStudentIdsAsync(int lessonDataId, int studentId)
        {
            return await _context.Doubts
                .Where(d => d.LessonDataId == lessonDataId && d.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<List<Doubt>> GetDoubtsByLessonDataIdAsync(int id)
        {
            return await _context.Doubts
                .Where(d => d.LessonDataId == id)
                .Include(d => d.Student)
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


        public async Task InsertDoubt(Doubt doubt)
        {
            _context.Doubts.Add(doubt);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoubtAsync(Doubt doubt)
        {
            _context.Doubts.Update(doubt);

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
