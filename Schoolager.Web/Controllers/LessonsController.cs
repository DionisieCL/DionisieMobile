using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Constants;
using Schoolager.Web.Data;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Helpers;
using Schoolager.Web.Models.Lessons;

namespace Schoolager.Web.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IRecurrenceHelper _recurrenceHelper;

        public LessonsController(
            ILessonRepository lessonRepository,
            IConverterHelper converterHelper,
            ITeacherRepository teacherRepository,
            ISubjectRepository subjectRepository,
            IRecurrenceHelper recurrenceHelper)
        {
            _lessonRepository = lessonRepository;
            _converterHelper = converterHelper;
            _teacherRepository = teacherRepository;
            _subjectRepository = subjectRepository;
            _recurrenceHelper = recurrenceHelper;
        }


        public async Task<IActionResult> Index()
        {
            var lessons = _lessonRepository.GetAll();


            return View(await lessons.ToListAsync());
        }

        public async Task<IActionResult> IndexTeacher()
        {
            return View();
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                // TODO: NotFoundViewResult
                return NotFound();
            }

            var lesson = await _lessonRepository.GetByIdAsync(id.Value);

            if (lesson == null)
            {
                // TODO: NotFoundViewResult
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create(string date)
        {
            DateTime dateTime;

            // If the user clicks the new appointment button
            if (date == null)
            {
                // Make sure slected time is 7 am
                TimeSpan time = new TimeSpan(8, 0, 0);

                // TODO: Change to first day of school
                dateTime = new DateTime(2022, 9, 15).Date + time;
                //dateTime = dateTime.Date + time;
            }
            else
            {
                // Get the time from the parsed in date
                dateTime = DateTime.Parse(date);
                TimeSpan time = dateTime.TimeOfDay;

                // TODO: Change to first day of school
                dateTime = new DateTime(2022, 9, 15).Date + time;
            }

            ViewData["SubjectId"] = _subjectRepository.GetComboSubjects();
            ViewData["DateString"] = dateTime.ToString("yyyy-MM-dd");
            ViewData["Date"] = dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

            LessonViewModel lessonViewModel = new LessonViewModel
            {
                StartTime = dateTime,
                EndTime = dateTime,
            };

            return View(lessonViewModel);
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LessonViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var subject = await _subjectRepository.GetByIdAsync(model.SubjectId);

                    if(subject == null)  
                    {
                        // TODO: NotFoundViewResult
                        return NotFound();
                    }

                    // TODO: Change to final day of school
                    model.RecurrenceRule = _recurrenceHelper.GetRecurrenceRule(new DateTime(2023, 12 ,1));
                    model.RecurrenceException = Holidays.GetStaticHolidays();
                    model.SubjectName = subject.Name;

                    var lesson = _converterHelper.ToLesson(model, true);

                    await _lessonRepository.CreateAsync(lesson);

                    // TODO: success message
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // TODO: failure message
                }
            }
            ViewData["SubjectId"] = _subjectRepository.GetComboSubjects();

            return View(model);
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                // TODO: NotFoundViewResult
                return NotFound();
            }

            var lesson = await _lessonRepository.GetByIdAsync(id.Value);

            if (lesson == null)
            {
                // TODO: NotFoundViewResult
                return NotFound();
            }

            var model = _converterHelper.ToLessonViewModel(lesson);

            ViewData["SubjectId"] = _subjectRepository.GetComboSubjects();
            ViewData["TeacherId"] = _teacherRepository.GetComboTeachersBySubjectId(model.SubjectId);
            ViewData["StartDate"] = model.StartTime.Value.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            ViewData["EndDate"] = model.EndTime.Value.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            
            return View(model);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LessonViewModel model)
        {
            if (id != model.Id)
            {
                // TODO: NotFoundViewResult
                return NotFound();
            }

            ViewData["SubjectId"] = _subjectRepository.GetComboSubjects();
            ViewData["TeacherId"] = _teacherRepository.GetComboTeachersBySubjectId(model.SubjectId);
            ViewData["StartDate"] = model.StartTime.Value.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            ViewData["EndDate"] = model.EndTime.Value.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

            if (ModelState.IsValid)
            {
                try
                {
                    var lesson = _converterHelper.ToLesson(model, false);

                    await _lessonRepository.UpdateAsync(lesson);

                    ViewData["StartDate"] = model.StartTime.Value.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                    ViewData["EndDate"] = model.EndTime.Value.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

                    // TODO: success message
                    return View(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _lessonRepository.ExistAsync(model.Id))
                    {
                        // TODO: NotFoundViewResult
                        return NotFound();
                    }
                    // TODO: failure message
                }
            }

            return View(model);
        }


        [HttpPost]
        [Route("Lessons/LessonDrag")]
        public async Task<JsonResult> LessonDrag(LessonViewModel model)
        {
            TimeSpan startTime = model.StartTime.Value.TimeOfDay;
            TimeSpan endTime = model.EndTime.Value.TimeOfDay;

            // Get first day of school
            model.StartTime = new DateTime(2022, 9, 15).Date + startTime;
            model.EndTime = new DateTime(2022, 9, 15).Date + endTime;

            model.RecurrenceException = Holidays.GetStaticHolidays();

            var lesson = _converterHelper.ToLesson(model, false);
            //TODO: Get Recurrence exception

            await _lessonRepository.UpdateAsync(lesson);

            return Json(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                // NotFoundViewResult("AppointmentNotFound");
                return NotFound();
            }

            var lesson = await _lessonRepository.GetByIdAsync(id.Value);

            if (lesson == null)
            {
                return NotFound();
            }

            try
            {
                await _lessonRepository.DeleteAsync(lesson);

                //_flashMessage.Confirmation("Appointment deleted successfully.");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (!await _lessonRepository.ExistAsync(lesson.Id))
                {
                    return NotFound();
                }
                //_flashMessage.Danger(ex.Message);
            }

            //_flashMessage.Danger("Could not delete appointment.");

            return RedirectToAction(nameof(Index));
        }
    }
}
