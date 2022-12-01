using Schoolager.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
        Task<Lesson> GetLessonByIdAsync(int id);
        IQueryable<Lesson> GetAllWithMembers();
        Task<List<Lesson>> GetLessonByTeacherIdAsync(int id);
        Task<LessonData> GetLessonData(int lessonId, DateTime lessonDate);
        Task<List<Lesson>> GetLessonByTurmaIdAsync(int id);
    }
}
