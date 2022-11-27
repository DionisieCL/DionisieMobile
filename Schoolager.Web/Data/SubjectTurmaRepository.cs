using Schoolager.Web.Data.Entities;

namespace Schoolager.Web.Data
{
    public class SubjectTurmaRepository : GenericRepository<SubjectTurma>, ISubjectTurmaRepository
    {
        private readonly DataContext _context;

        public SubjectTurmaRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
