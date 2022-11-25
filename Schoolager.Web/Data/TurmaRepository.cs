using Schoolager.Web.Data.Entities;

namespace Schoolager.Web.Data
{
    public class TurmaRepository : GenericRepository<Turma>, ITurmaRepository
    {
        private readonly DataContext _context;

        public TurmaRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
