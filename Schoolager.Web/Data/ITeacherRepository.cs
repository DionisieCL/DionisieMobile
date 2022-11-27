using Schoolager.Web.Data.Entities;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        Task<Teacher> GetTeacherWithSubject(int id);
    }
}