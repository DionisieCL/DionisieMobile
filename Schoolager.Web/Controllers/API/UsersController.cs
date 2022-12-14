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
        public IActionResult GetLessonById()
        {
            // Get the logged in user to check if it's a teacher or a student
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = "897250d8-c382-4de2-b241-0b5a08e71491";
            if (User.IsInRole("Teacher"))
            {
                var teacher = _teacherRepository.GetByUserIdAsync(userId);

                if (teacher == null)
                {
                    //TODO: return new NotFoundViewresult("TeacherNotFound");
                    return NotFound();
                }

                var lessons =_lessonRepository.GetLessonByTeacherIdAsync(teacher.Id);

                return Ok(lessons);
            }
            else if (User.IsInRole("Student"))
            {
                var student =  _studentRepository.GetByUserIdAsync(userId);

                if (student == null)
                {
                    //TODO: return new NotFoundViewresult("StudentNotFound");
                    return NotFound();
                }

                var lessons = _lessonRepository.GetLessonByStudentIdAsync(student.Id);

                return Ok(lessons);
            }

            return NotFound();
        }
    }
}