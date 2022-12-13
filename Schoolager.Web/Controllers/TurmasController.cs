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
using Schoolager.Web.Models.Students;
using Schoolager.Web.Models.Turmas;

namespace Schoolager.Web.Controllers
{
    [Authorize(Roles ="Employee,Admin")]
    public class TurmasController : Controller
    {

        private readonly ITurmaRepository _turmaRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISubjectTurmaRepository _subjectTurmaRepository;
        private readonly ITeacherRepository _teacherRepositry;
        private readonly ITeacherTurmaRepository _teacherTurmaRepository;

        public TurmasController(
            ITurmaRepository turmaRepository,
            IStudentRepository studentRepository,
            IConverterHelper converterHelper,
            ISubjectRepository subjectRepository,
            ISubjectTurmaRepository subjectTurmaRepository,
            ITeacherRepository teacherRepositry,
            ITeacherTurmaRepository teacherTurmaRepository)
        {
            _turmaRepository = turmaRepository;
            _studentRepository = studentRepository;
            _converterHelper = converterHelper;
            _subjectRepository = subjectRepository;
            _subjectTurmaRepository = subjectTurmaRepository;
            _teacherRepositry = teacherRepositry;
            _teacherTurmaRepository = teacherTurmaRepository;
        }

        // GET: Turmas
        public async Task<IActionResult> Index()
        {
            var turmas = _turmaRepository.GetAll();

            return View(await turmas.ToListAsync());
        }

        // GET: Turmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _turmaRepository.GetByIdAsync(id.Value);
            if (turma == null)
            {
                return NotFound();
            }

            return View(turma);
        }

        // GET: Turmas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Turmas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Turma turma)
        {
            if (ModelState.IsValid)
            {
                await _turmaRepository.CreateAsync(turma);

                return RedirectToAction(nameof(Index));
            }

            return View(turma);
        }

        public async Task<IActionResult> AddStudents(int? id)
        {
            if (id == null)
            {
                //TODO:return new NotFoundViewResult("TurmaNotFound");
                return NotFound();
            }
            var turma = _turmaRepository.GetWithStudentsById(id.Value);

            if (turma == null)
            {
                //TODO:return new NotFoundViewResult("TurmaNotFound");
                return NotFound();
            }

            var turmaStudents = _studentRepository.GetByTurmaId(turma.Id);
            var students = _studentRepository.GetFreeStudentsBySchoolYear(turma.SchoolYear);
            //var students = _studentRepository.GetFreeStudents();

            AddStudentsViewModel model = new AddStudentsViewModel
            {
                FreeStudents = _converterHelper.AllToStudentViewModel(students),
                TurmaStudents = _converterHelper.AllToStudentViewModel(turmaStudents),
            };

            ViewData["TurmaId"] = turma.Id;
            ViewData["TurmaName"] = turma.Name;

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> ManageStudents(List<StudentViewModel> model, int? turmaId)
        {
            List<Student> students = _converterHelper.AllToStudent(model, turmaId);

            try
            {
                await _studentRepository.UpdateRangeAsync(students);
            }
            catch (Exception ex)
            {

                throw;
            }

            return Json(model);
        }


        // GET: Turmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _turmaRepository.GetByIdAsync(id.Value);

            if (turma == null)
            {
                return NotFound();
            }
            return View(turma);
        }

        // POST: Turmas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Turma turma)
        {
            if (id != turma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _turmaRepository.UpdateAsync(turma);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _turmaRepository.ExistAsync(turma.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(turma);
        }

        // GET: Turmas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _turmaRepository.GetByIdAsync(id.Value);
            if (turma == null)
            {
                return NotFound();
            }

            return View(turma);
        }

        // POST: Turmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turma = await _turmaRepository.GetByIdAsync(id);
            await _turmaRepository.DeleteAsync(turma);
            return RedirectToAction(nameof(Index));
        }

        //Teste adicionar Subjects a uma Turma
        public async Task<IActionResult> AddSubjectsToTurmaAsync(int? id)
        {

            var allSubjects = await _subjectRepository.GetAll().ToListAsync();

            var subjectsInTurma = await _subjectTurmaRepository.GetAllSubjectsWithTurma(id.Value);

            List<Subject> available = allSubjects.Where(st => !subjectsInTurma.Any(al => al.Id == st.Id)).ToList();

            var model = new AddSubjectsToTurmaViewModel
            {
                TurmaId = id.Value,
                AvailableSubjects = available,
                SubjectsInTurma = subjectsInTurma
            };

            var turma = await _turmaRepository.GetByIdAsync(id.Value);

            if (turma == null)
            {
                //TODO:return new NotFoundViewResult("TurmaNotFound");
                return NotFound();
            }


            ViewData["TurmaId"] = id.Value;
            ViewData["TurmaName"] = turma.Name;
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> AddSubjects(List<Subject> subjects, int turmaId)
        {
            List<SubjectTurma> subTurmas = new List<SubjectTurma>();

            foreach (var item in subjects)
            {
                subTurmas.Add(new SubjectTurma
                {
                    SubjectId = item.Id,
                    TurmaId = turmaId
                });
            }

            try
            {
                await _subjectTurmaRepository.InsertSubjectTurmasAsync(subTurmas);
            }
            catch (Exception ec)
            {
                throw;
            }


            return Json(subjects);
        }

        [HttpPost]
        public async Task<JsonResult> RemoveSubjects(List<Subject> subjects, int turmaId)
        {

            List<SubjectTurma> subTurmas = new List<SubjectTurma>();

            foreach (var item in subjects)
            {
                subTurmas.Add(new SubjectTurma
                {
                    SubjectId = item.Id,
                    TurmaId = turmaId
                });
            }

            try
            {
                await _subjectTurmaRepository.RemoveSubjectTurmasAsync(subTurmas);
            }
            catch (Exception ec)
            {
                throw;
            }

            return Json(subjects);
        }

        public async Task<IActionResult> AddTeachersToTurma(int? id)
        {

            if (id == null)
            {
                return View();
            }

            var subjects = await _turmaRepository.GetTurmaSubjects(id.Value);

            var model = new AddTeacherToTurmaViewModel();

            model.SubjectTurmaViewModels = new List<SubjectTurmaViewModel>();

            foreach (var item in subjects)
            {
                var teacher = _teacherTurmaRepository.GetTeacherWithSubjectInTurma(id.Value, item.Id);

                model.SubjectTurmaViewModels.Add(new SubjectTurmaViewModel
                {
                    Subject = item,
                    SubjectId = item.Id,
                    Teachers = _teacherRepositry.GetComboTeachersBySubjectId(item.Id),
                    TeacherId = teacher == null ? 0 : teacher.Id,
                });
            }

            var turma = await _turmaRepository.GetByIdAsync(id.Value);

            if (turma == null)
            {
                //TODO:return new NotFoundViewResult("TurmaNotFound");
                return NotFound();
            }


            ViewData["TurmaId"] = id.Value;
            ViewData["TurmaName"] = turma.Name;

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddTeachersToTurma(AddTeacherToTurmaViewModel model)
        {
            // Delete existing records
            await _teacherTurmaRepository.DeleteAllByTurmaId(model.TurmaId);

            List<TeacherTurma> teacherTurmas = new List<TeacherTurma>();

            foreach (var item in model.SubjectTurmaViewModels)
            {
                if(item.TeacherId != 0)
                {
                    teacherTurmas.Add(new TeacherTurma
                    {
                        TeacherId=item.TeacherId,
                        TurmaId = model.TurmaId,
                        SubjectId = item.SubjectId,
                    });
                }
            }
            
            await _teacherTurmaRepository.InsertTeacherTurmaAsync(teacherTurmas);


            var subjects = await _turmaRepository.GetTurmaSubjects(model.TurmaId);

            model.SubjectTurmaViewModels = new List<SubjectTurmaViewModel>();

            foreach (var item in subjects)
            {
                var teacher = _teacherTurmaRepository.GetTeacherWithSubjectInTurma(model.TurmaId, item.Id);

                model.SubjectTurmaViewModels.Add(new SubjectTurmaViewModel
                {
                    Subject = item,
                    Teachers = _teacherRepositry.GetComboTeachersBySubjectId(item.Id),
                    TeacherId = teacher == null ? 0 : teacher.Id,
                });
            }

            var turma = await _turmaRepository.GetByIdAsync(model.TurmaId);

            if (turma == null)
            {
                //TODO:return new NotFoundViewResult("TurmaNotFound");
                return NotFound();
            }


            ViewData["TurmaId"] = model.TurmaId;
            ViewData["TurmaName"] = turma.Name;

            return View(model);
        }
    }
}
