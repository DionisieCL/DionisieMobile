using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Helpers;
using Schoolager.Web.Models.Grades;
using Schoolager.Web.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Schoolager.Web.Controllers
{
    [Authorize]
    public class GradesController : Controller
    {
        private readonly DataContext _context;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;

        public GradesController(
            DataContext context,
            ITurmaRepository turmaRepository,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository,
            IGradeRepository gradeRepository,
            IConverterHelper converterHelper,
            IUserHelper userHelper)
        {
            _context = context;
            _turmaRepository = turmaRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _gradeRepository = gradeRepository;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
        }

        public IActionResult Index()
        {
            return View(_turmaRepository.GetAll());
        }

        public async Task<IActionResult> TeacherIndex(int id)
        {
            // Get the logged in user to check if it's an owner
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var teacher = await _teacherRepository.GetByUserIdAsync(userId);

            if (teacher == null)
            {
                //TODO: return new NotFoundViewresult("TeacherNotFound");
                return NotFound();
            }

            var turmas = await _turmaRepository.GetTurmasByTeacherIdAsync(teacher.Id);

            return View(turmas);
        }

        public async Task<IActionResult> TurmaGrading(int? id)
        {
            if (id == null)
            {
                //TODO: return new NotFoundViewresult("TurmaNotFound");
                return NotFound();
            }

            // Get the logged in user to check if it's an owner
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var teacher = await _teacherRepository.GetByUserIdWithSubjectAsync(userId);

            if (teacher == null)
            {
                //TODO: return new NotFoundViewresult("TeacherNotFound");
                return NotFound();
            }

            // TODO: From database
            GradesViewModel model = new GradesViewModel();

            model.GradeViewModels = new List<GradeViewModel>();

            var students = _studentRepository.GetByTurmaId(id.Value);

            List<int> studentIds = students.Select(s => s.Id).ToList();
            var grades = await _gradeRepository.GetGradesBySubjectAndStudentIdsAsync(teacher.SubjectId.Value, studentIds);

            for (int i = 0; i < students.Count; i++)
            {
                model.GradeViewModels.Add(new GradeViewModel
                {
                    StudentViewModel = _converterHelper.ToStudentViewModel(students[i]),
                    FirstTermMark = grades.Count == 0 ? 0 : grades[i].FirstMark,
                    SecondTermMark = grades.Count == 0 ? 0 : grades[i].SecondMark,
                    ThirdTermMark = grades.Count == 0 ? 0 : grades[i].FirstMark,
                });
            }

            ViewData["SubjectId"] = teacher.SubjectId;
            ViewData["SubjectName"] = teacher.Subject.Name;
            ViewData["SubjectName"] = teacher.Subject.Name;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TurmaGrading(GradesViewModel model)
        {
            List<int> studentIds = model.GradeViewModels.Select(gvm => gvm.StudentId).ToList();

            var gradesDb = await _gradeRepository.GetGradesBySubjectAndStudentIdsAsync(model.SubjectId, studentIds);

            List<Grade> grades = new List<Grade>(); 

            foreach(var grade in model.GradeViewModels)
            {
                grades.Add(new Grade 
                { 
                    SubjectId = model.SubjectId, 
                    StudentId = grade.StudentId,
                    FirstMark = grade.FirstTermMark,
                    SecondMark = grade.SecondTermMark,
                    ThirdMark = grade.ThirdTermMark,
                });
            }

            if(gradesDb.Count == 0)
            {
                await _gradeRepository.InsertGradesAsync(grades);
            } else
            {
                for(int i = 0; i < gradesDb.Count; i++)
                {
                    gradesDb[i].FirstMark = model.GradeViewModels[i].FirstTermMark;
                    gradesDb[i].SecondMark = model.GradeViewModels[i].SecondTermMark;
                    gradesDb[i].ThirdMark = model.GradeViewModels[i].ThirdTermMark;
                }

                await _gradeRepository.UpdateGradesAsync(gradesDb);
            }

            return RedirectToAction(nameof(TurmaGrading), new {id = model.TurmaId});
        }

        //public async Task<IActionResult> ShowStudentsInTurma(int? id)
        //{
        //    return View(await _studentRepository.GetStudentWithTurma(id.Value));
        //}

        //public async Task<ActionResult> ShowAllStudentGrades(int? id)
        //{
        //    return View(await _gradeRepository.GetGradesWithStudent(id.Value));
        //}

        public async Task<IActionResult> ShowLoggedStudentGrades()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            if(user == null)
            {
                return View();
            }

            var grades = await _gradeRepository.GetLoggedStudentGrades(user.UserName);

            return View(grades);
        }
    }
}
