using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Helpers;
using Vereyon.Web;

namespace Schoolager.Web.Controllers
{
    [Authorize(Roles = "Employee,Admin")]
    public class RoomsController : Controller
    {
        private readonly DataContext _context;
        private readonly IRoomRepository _roomRepository;
        private readonly IFlashMessage _flashMessage;

        public RoomsController(DataContext context,
            IRoomRepository roomRepository,
            IFlashMessage flashMessage)
        {
            _context = context;
            _roomRepository = roomRepository;
            _flashMessage = flashMessage;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rooms.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("RoomNotFound");
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return new NotFoundViewResult("RoomNotFound");
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();

                _flashMessage.Confirmation("Room Created.");

                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("RoomNotFound");
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return new NotFoundViewResult("RoomNotFound");
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Room room)
        {
            if (id != room.Id)
            {
                return new NotFoundViewResult("RoomNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                    _flashMessage.Confirmation("Room Updated.");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!RoomExists(room.Id))
                    {
                        return new NotFoundViewResult("RoomNotFound");
                    }
                    else
                    {
                        _flashMessage.Danger(ex.Message);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("RoomNotFound");
            }

            var room = await _roomRepository.GetByIdAsync(id.Value);
            if (room == null)
            {
                return new NotFoundViewResult("RoomNotFound");
            }

            try
            {
                await _roomRepository.DeleteAsync(room);

                _flashMessage.Confirmation("Room Deleted Successfully.");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"Something went wrong while trying to delete the room {room.Name}.";
                    ViewBag.ErrorMessage = $"Please try again in a few moments or contact the system administrators.</br></br>";
                }

                return View("Error");
            }

        }

        //// POST: Rooms/Delete/5
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var room = await _roomRepository.GetByIdAsync(id);

        //}

        public IActionResult RoomNotFound()
        {
            return View();
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
