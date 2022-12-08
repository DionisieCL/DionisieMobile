using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
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
        private readonly ITurmaRepository _turmaRepository;
        private readonly IRecurrenceHelper _recurrenceHelper;
        private readonly ILessonDataRepository _lessonDataRepository;
        private readonly IStudentRepository _studentRepository;

        public LessonsController(
            ILessonRepository lessonRepository,
            IConverterHelper converterHelper,
            ITeacherRepository teacherRepository,
            ISubjectRepository subjectRepository,
            ITurmaRepository turmaRepository,
            IRecurrenceHelper recurrenceHelper,
            ILessonDataRepository lessonDataRepository,
            IStudentRepository studentRepository)
        {
            _lessonRepository = lessonRepository;
            _converterHelper = converterHelper;
            _teacherRepository = teacherRepository;
            _subjectRepository = subjectRepository;
            _turmaRepository = turmaRepository;
            _recurrenceHelper = recurrenceHelper;
            _lessonDataRepository = lessonDataRepository;
            _studentRepository = studentRepository;
        }

        public async Task<IActionResult> TurmaSchedule(int id)
        {
            var turma = await _turmaRepository.GetByIdAsync(id);

            if (turma == null)
            {
                // new NotFoundViewResult("TurmaNotFound");
                return NotFound();
            }

            ViewData["TurmaId"] = id;
            ViewData["TurmaName"] = turma.Name;

            var lessons = await _lessonRepository.GetLessonByTurmaIdAsync(id);

            return View(lessons);
        }

        public async Task<IActionResult> TeacherIndex()
        {
            // Get the logged in user to check if it's an owner
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var teacher = await _teacherRepository.GetByUserIdAsync(userId);

            if (teacher == null)
            {
                //TODO: return new NotFoundViewresult("TeacherNotFound");
                return NotFound();
            }

            var lessons = await _lessonRepository.GetLessonByTeacherIdAsync(teacher.Id);
            //var lessonsList = await lessons.ToListAsync();

            return View(lessons);
        }


        //public

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
        public async Task<IActionResult> Create(int id, string date)
        {
            var turma = await _turmaRepository.GetByIdAsync(id);

            if(turma == null)
            {
                // new NotFoundViewResult("TurmaNotFound");
                return NotFound();
            }

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

            ViewData["SubjectId"] = _subjectRepository.GetComboSubjectsByTurmaId(id);
            ViewData["DateString"] = dateTime.ToString("yyyy-MM-dd");
            ViewData["Date"] = dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            ViewData["TurmaId"] = id;

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
                    return RedirectToAction(nameof(TurmaSchedule), new { id = model.TurmaId });
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
                // NotFoundViewResult("AppointmentNotFound");
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

        //[HttpGet("details")]
        public async Task<IActionResult> LessonData(int lessonId, string date)
        {
            DateTime lessonDate = DateTime.Parse(date);

            var lessonData = await _lessonRepository.GetLessonData(lessonId, lessonDate);

            LessonDataViewModel model = new LessonDataViewModel();

            if (lessonData == null)
            {
                lessonData = new LessonData()
                {
                    LessonDate = lessonDate,
                    LessonId = lessonId,
                };

                try
                {
                    await _lessonDataRepository.CreateAsync(lessonData);

                    model.Id = lessonData.Id;
                    model.LessonId = lessonId;

                    return View(model);
                }
                catch (Exception ex)
                {
                    return RedirectToAction(nameof(TeacherIndex));
                }

                // TODO: return create new View
                return RedirectToAction(nameof(CreateLessonData), new { id = lessonId});
            }

            model.Id = lessonData.Id;
            model.LessonId = lessonData.LessonId;
            model.Summary = lessonData.Summary;

            // TODO: return create new View
            return View(model);
        }


        public async Task<IActionResult> LessonDataAbsences(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);

            if(lesson == null)
            {
                // NotFoundViewResult("AppointmentNotFound");
                return NotFound();
            }

            var students = _studentRepository.GetByTurmaId(lesson.TurmaId);

            LessonDataViewModel model = new LessonDataViewModel();

            model.Attendances = new List<AttendanceViewModel>();

            foreach (var student in students)
            {
                model.Attendances.Add(new AttendanceViewModel()
                {
                    StudentViewModel = _converterHelper.ToStudentViewModel(student),
                });
            }

            return View(model);
        }

        public IActionResult LessonDataResources(int id)
        {
            return View();
        }

        public IActionResult LessonDataHomework(int id)
        {
            return View();
        }


        public async Task<IActionResult> CreateLessonData(int id)
        {
            var turma = _turmaRepository.GetWithStudentsById(id);

            LessonDataViewModel model = new LessonDataViewModel();

            model.Attendances = new List<AttendanceViewModel>();

            foreach (var student in turma.Students)
            {
                model.Attendances.Add(new AttendanceViewModel
                {
                    StudentViewModel = _converterHelper.ToStudentViewModel(student),
                });
            }

            return View(model);
        }

        public async Task<IActionResult> EditLessonData(int id)
        {
            return View();
        }
    }
}
