using Schoolager.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
        Task<Lesson> GetLessonByIdAsync(int id);
        IQueryable<Lesson> GetAllWithMembers();
    }
}
