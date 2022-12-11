using Microsoft.AspNetCore.Mvc.Rendering;
using Schoolager.Web.Data.Entities;
using System;
using System.Collections.Generic;

namespace Schoolager.Web.Data
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        IEnumerable<SelectListItem> GetComboAvailableRooms(DateTime startTime, DateTime endTime, int weekDay);
    }
}
