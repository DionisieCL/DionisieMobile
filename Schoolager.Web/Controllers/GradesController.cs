using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data;
using Schoolager.Web.Models.Students;
using System.Threading.Tasks;

namespace Schoolager.Web.Controllers
{
    public class GradesController : Controller
    {
        private readonly DataContext _context;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IGradeRepository _gradeRepository;

        public GradesController(DataContext context,
                                ITurmaRepository turmaRepository,
                                IStudentRepository studentRepository,
                                IGradeRepository gradeRepository)
        {
            _context = context;
            _turmaRepository = turmaRepository;
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
        }

        public IActionResult Index()
        {
            return View(_turmaRepository.GetAll());
        }

        public async Task<IActionResult> ShowStudentsInTurma(int? id)
        {
            return View(await _studentRepository.GetStudentWithTurma(id.Value));
        }

        public async Task<ActionResult> ShowAllStudentGrades(int? id)
        {
            return View(await _gradeRepository.GetGradesWithStudent(id.Value));
        }
    }
}
