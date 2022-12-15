using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Helpers;

namespace Schoolager.Web.Controllers
{
    public class HolidaysController : Controller
    {
        private readonly IHolidayRepository _holidayRepository;

        public HolidaysController(
            IHolidayRepository holidayRepository)
        {
            _holidayRepository = holidayRepository;
        }

        // GET: Holidays
        public async Task<IActionResult> Index()
        {
            return View(await _holidayRepository.GetAll().ToListAsync());
        }


        // GET: Holidays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Holidays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Holiday holiday)
        {
            if (ModelState.IsValid)
            {
                await _holidayRepository.CreateAsync(holiday);

                return RedirectToAction(nameof(Index));
            }
            return View(holiday);
        }

        // GET: Holidays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("HolidayNotFound");
            }

            var holiday = await _holidayRepository.GetByIdAsync(id.Value);

            if (holiday == null)
            {
                return new NotFoundViewResult("HolidayNotFound");
            }
            return View(holiday);
        }

        // POST: Holidays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartTime,EndTime")] Holiday holiday)
        {
            if (id != holiday.Id)
            {
                return new NotFoundViewResult("HolidayNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _holidayRepository.UpdateAsync(holiday);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _holidayRepository.ExistAsync(id))
                    {
                        return new NotFoundViewResult("HolidayNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(holiday);
        }

        // GET: Holidays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("HolidayNotFound");
            }

            var holiday = await _holidayRepository.GetByIdAsync(id.Value);

            if (holiday == null)
            {
                return new NotFoundViewResult("HolidayNotFound");
            }

            await _holidayRepository.DeleteAsync(holiday);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult HolidayNotFound()
        {
            return View();
        }
    }
}
