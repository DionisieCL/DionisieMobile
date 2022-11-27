using Microsoft.AspNetCore.Mvc;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;

namespace Schoolager.Web.Controllers
{
    public class FrontendTestsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddStudentsToTurma()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddStudents(List<Student> students, int turmaId)
        {
            return Json(students);
        }

        [HttpPost]
        public JsonResult RemoveStudents(List<Student> students, int turmaId)
        {
            return Json(students);
        }

        public IActionResult EditSchedule()
        {
            return View();
        }
    }
}
