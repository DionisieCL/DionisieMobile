using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Helpers;
using Schoolager.Web.Models.Teachers;
using Vereyon.Web;

namespace Schoolager.Web.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMailHelper _mailHelper;
        private readonly IFlashMessage _flashMessage;
        private readonly IUserHelper _userHelper;

        public TeachersController(                      
            ITeacherRepository teacherRepository,
            IConverterHelper converterHelper,
            IBlobHelper blobHelper,
            ISubjectRepository subjectRepository,
            IMailHelper mailHelper,
            IFlashMessage flashMessage,
            IUserHelper userHelper)
        {
            _teacherRepository = teacherRepository;
            _converterHelper = converterHelper;
            _blobHelper = blobHelper;
            _subjectRepository = subjectRepository;
            _mailHelper = mailHelper;
            _flashMessage = flashMessage;
            _userHelper = userHelper;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherRepository.GetWithSubjectsAsync();

            return View(teachers);
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherRepository.GetByIdAsync(id.Value);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = _subjectRepository.GetComboSubjects();

            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = Guid.Empty;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "teachers");
                    }

                    var user = await _userHelper.GetUserByEmailAsync(model.Email);

                    if (user == null)
                    {
                        var teacher = _converterHelper.ToTeacher(model, imageId, true);

                        user = _converterHelper.ToUser(teacher, new User(), "teachers");

                        var password = Guid.NewGuid().ToString();

                        var result = await _userHelper.AddUserAsync(user, password);

                        if (result != IdentityResult.Success)
                        {
                            throw new Exception("The user could not be created, please try again.");
                        }

                        // Add role or roles to user
                        await _userHelper.AddUserToRoleAsync(user, "Teacher");

                        // get the newly created user and set it as the teacher's user
                        teacher.User = await _userHelper.GetUserByEmailAsync(model.Email);

                        await _teacherRepository.CreateAsync(teacher);

                        Response response = await ConfirmEmailAsync(user, model);

                        if (response.IsSuccess)
                        {
                            _flashMessage.Confirmation("The teacher has been created and confirmation email has been sent to user.");
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    _flashMessage.Danger("That email is already being used by another user.");

                    return View(model);
                }
                catch (Exception ex)
                {
                    _flashMessage.Danger(ex.Message);
                }
            }
            ViewData["SubjectId"] = _subjectRepository.GetComboSubjects();

            return View(model);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherRepository.GetWithUserByIdAsync(id.Value);

            if (teacher == null)
            {
                return NotFound();
            }

            ViewData["SubjectId"] = _subjectRepository.GetComboSubjects();

            var model = _converterHelper.ToTeacherViewModel(teacher);

            return View(model);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeacherViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "teachers");
                    }

                    var user = await _userHelper.GetUserByIdAsync(model.UserId);

                    if (user == null)
                    {
                        //return new NotFoundViewResult("TeacherNotFound");
                        return NotFound();
                    }

                    var teacher = _converterHelper.ToTeacher(model, imageId, false);

                    user = _converterHelper.ToUser(teacher, user, "teachers");

                    var response = await _userHelper.UpdateUserAsync(user);

                    if (response.Succeeded)
                    {
                        model.User = user;

                        teacher.UserId = user.Id;

                        await _teacherRepository.UpdateAsync(teacher);

                        _flashMessage.Confirmation("Teacher has been updated.");

                        return View(model);
                    }

                    _flashMessage.Danger("An error ocurred whilst tryng to update the owner, please try again.");

                    return View(model);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (! await _teacherRepository.ExistAsync(model.Id))
                    {
                        //return new NotFoundViewResult("TeacherNotFound");
                        return NotFound();
                    }
                    else
                    {
                        _flashMessage.Danger(ex.Message);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = _subjectRepository.GetComboSubjects();

            return View(model);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherRepository.GetByIdAsync(id.Value);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            await _teacherRepository.DeleteAsync(teacher);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Teachers/GetTeachersBySubjectIdAsync")]
        public async Task<JsonResult> GetTeachersBySubjectIdAsync(int subjectId)
        {
            var teachers =  _teacherRepository.GetTeachersBySubjectId(subjectId);

            return Json(await teachers.ToListAsync());
        }

        public async Task<Response> ConfirmEmailAsync(User user, TeacherViewModel model)
        {
            string confirmationToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            string passwordToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
            string tokenLink = Url.Action(
                "ConfirmEmail",
                "Account", new
                {
                    userId = user.Id,
                    confirmationToken = confirmationToken,
                    passwordToken = passwordToken

                }, protocol: HttpContext.Request.Scheme);

            Response response = _mailHelper.SendEmail(
                model.Email,
                "Activate Account",
                $"<h1>Email Confirmation</h1>" +
                $"To activate your account please click the link and set up a new password:</br></br><a href = \"{tokenLink}\">Confirm Account</a>");

            return response;
        }
    }
}
