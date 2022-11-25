using Schoolager.Web.Data.Entities;

namespace Schoolager.Web.Data
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        private readonly DataContext _context;

        public TeacherRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
