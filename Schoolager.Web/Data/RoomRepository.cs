using Microsoft.AspNetCore.Mvc.Rendering;
using Schoolager.Web.Data.Entities;
using System;
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

            //list.Insert(0, new SelectListItem
            //{
            //    Text = "< Select a Room >",
            //    Value = null,
            //});
            //
            return list;
        }

        public IEnumerable<SelectListItem> GetComboAvailableRooms(DateTime startTime, DateTime endTime, int weekDay)
        {
            var salasTodas = _context.Rooms.ToList();

            var salasASerUsadas = _context.Lessons
                .Where(l => l.StartTime != startTime && l.EndTime != endTime && l.WeekDay == weekDay)
                .Select(r => r.Room);

            List<Room> available = salasTodas.Where(st => !salasASerUsadas.Any(al => al.Id == st.Id)).ToList();

            var list = _context.Rooms
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString(),
                }).OrderBy(r => r.Text).ToList();

            return list;
        }

    }
}
