using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schoolager.Web.Helpers;
using Schoolager.Web.Models.Account;
using Schoolager.Prism.ViewModels;
using System.Globalization;
using Org.BouncyCastle.Security;
using static System.Net.Mime.MediaTypeNames;
using Schoolager.Web.Data.Entities;
using System.Security.Claims;
using Schoolager.Web.Data;
using System.ComponentModel.DataAnnotations;

namespace Schoolager.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IStudentRepository _studentRepository;


        public UsersController(IUserHelper userHelper, 
            ITeacherRepository teacherRepository,
               ILessonRepository lessonRepository,
               IStudentRepository studentRepository)
        {
            _userHelper = userHelper;
            _teacherRepository = teacherRepository;
            _lessonRepository = lessonRepository;
            _studentRepository = studentRepository;

        }
        
        [HttpGet]
        [Route("GetAll")]
        public  IActionResult GetUsers()
        {
          
            //  var result =  _userHelper.GetAll();
           var result = _userHelper.GetUserByEmailAsync("student@mailinator.com");
            return Ok(result.Result);
              
        }
        [HttpPost]
        [Route("GetUserByEmail")]
        public IActionResult Login([Required] string email, [Required]string password)
        {
            LoginViewModel model = new LoginViewModel();
            model.Username = email;
            model.Password = password;
            var result =  _userHelper.LoginAsync(model);
            if (result.Result.Succeeded)
            {
                var user = _userHelper.GetUserByEmailAsync(model.Username);

                return Ok(user.Result);
            }

            return NoContent();
        }

        [HttpGet]
        [Route("GetLessonsById")]
        public IActionResult GetLessonById(int id)
        {
            // Get the logged in user to check if it's a teacher or a student
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //student Id a8072336-9efc-4e6c-b14c-f66035f8d3d1
            //teacher Id 2a842e35-d899-4f1d-9221-bdd39f9f2df8
            var userId = id;
            //  var userEmail = _userHelper.GetUserByEmailAsync(email);
            //var user = _userHelper.GetUserByIdAsync(userId).Result;
            // var student = _studentRepository.GetByUserIdAsync(id);
            //var lessons = _lessonRepository.GetLessonByStudentIdAsync(1);
           // var student = _studentRepository.GetByUserIdAsync(id);
            var lessons = _lessonRepository.GetLessonByStudentIdAsync(2);
            return Ok(lessons.Result);
            /*
            if (_userHelper.IsInRoleAsync(user, "Teacher").Result)
            {
                var teacher = _teacherRepository.GetByUserIdAsync(user.Id);

                if (teacher == null)
                {
                    //TODO: return new NotFoundViewresult("TeacherNotFound");
                    return NotFound();
                }

                var lessons =_lessonRepository.GetLessonByTeacherIdAsync(teacher.Result.Id);

                return Ok(lessons.Result);
            }
            else if (_userHelper.IsInRoleAsync(user, "Student").Result)
            {
                var student = _studentRepository.GetByUserIdAsync(user.Id);

                if (student == null)
                {
                    //TODO: return new NotFoundViewresult("StudentNotFound");
                    return NotFound();
                }

                var lessons = _lessonRepository.GetLessonByStudentIdAsync((int)student.Result.TurmaId);

                return Ok(lessons.Result);
            }

            return NotFound();*/
            
        }
    }
}