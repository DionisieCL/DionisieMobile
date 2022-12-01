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

        public GradesController(DataContext context,
                                ITurmaRepository turmaRepository,
                                IStudentRepository studentRepository)
        {
            _context = context;
            _turmaRepository = turmaRepository;
            _studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            return View(_turmaRepository.GetAll());
        }

        public async Task<IActionResult> ShowStudentsInTurma(int? id)
        {
            return View(await _studentRepository.GetStudentWithTurma(id.Value));
        }

        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
            return View();
        }

    }
}
