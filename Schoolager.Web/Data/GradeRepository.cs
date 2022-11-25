using Schoolager.Web.Data.Entities;

namespace Schoolager.Web.Data
{
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        private readonly DataContext _context;

        public GradeRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
