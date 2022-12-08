using Schoolager.Web.Data.Entities;

namespace Schoolager.Web.Data
{
    public class LessonDataRepository : GenericRepository<LessonData>, ILessonDataRepository
    {
        public LessonDataRepository(DataContext context) : base(context)
        {

        }
    }
}
