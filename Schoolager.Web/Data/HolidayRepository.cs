using Schoolager.Web.Data.Entities;

namespace Schoolager.Web.Data
{
    public class HolidayRepository : GenericRepository<Holiday>, IHolidayRepository
    {
        private readonly DataContext _context;

        public HolidayRepository(DataContext context) : base(context) 
        {
            _context = context;
        }
    }
}
