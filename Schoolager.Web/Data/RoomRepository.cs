using Microsoft.AspNetCore.Mvc.Rendering;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Schoolager.Web.Data
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        private readonly DataContext _context;

        public RoomRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboRooms()
        {
            var list = _context.Rooms.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString(),
            }).OrderBy(r => r.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "< Select a Room >",
                Value = null,
            });

            return list;
        }

        //public IEnumerable<SelectListItem> GetComboAvailableRooms()
        //{
        //    var list = _context.Rooms
        //        .Where()
        //        .Select(r => new SelectListItem
        //        {
        //        Text = r.Name,
        //        Value = r.Id.ToString(),
        //    }).OrderBy(r => r.Text).ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "< Select a Room >",
        //        Value = null,
        //    });

        //    return list;
        //}

    }
}
