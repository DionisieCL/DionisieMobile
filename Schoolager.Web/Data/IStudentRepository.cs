using Schoolager.Web.Data.Entities;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<Student> GetStudentWithTurma(int id);
    }
}
