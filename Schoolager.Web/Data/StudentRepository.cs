using Schoolager.Web.Data.Entities;

namespace Schoolager.Web.Data
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {

        private readonly DataContext _context;

        public StudentRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}
