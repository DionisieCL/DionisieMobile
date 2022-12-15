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
        public IActionResult GetUsers()
        {

            var result = _userHelper.GetAll();
            return Ok(result);

        }
        [HttpPost]
        [Route("GetUserByEmail")]
        public IActionResult Login([FromBody] TestUser testuser)
        {
            LoginViewModel model = new LoginViewModel();
            model.Username = testuser.Email;
            model.Password = testuser.Password;
            var result = _userHelper.LoginAsync(model);
            if (result.Result.Succeeded)
            {
                var user = _userHelper.GetUserByEmailAsync(model.Username);

                return Ok(user.Result);
            }

            return NoContent();
        }

        [HttpGet]
        [Route("GetLessonsById")]
        public async Task<IActionResult> GetLessonById([FromBody] string userId)
        {
            var student = await _studentRepository.GetByUserIdAsync(userId);
            var teacher = await _teacherRepository.GetByUserIdAsync(userId);
            var lessons = await _lessonRepository.GetLessonByTeacherIdAsync(teacher.Id);
            return Ok(lessons);
            /*
            if (teacher != null)
            {
                var lessons = await _lessonRepository.GetLessonByTeacherIdAsync(teacher.Id);
                return Ok(lessons);
            }
            if (student != null)
            {
                var lessons = await _lessonRepository.GetLessonByStudentIdAsync((int)student.TurmaId);
                return Ok(lessons);
            }

            return NotFound();*/
        }
    }
}