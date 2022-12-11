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
using Schoolager.Web.Models.Students;
using Vereyon.Web;

namespace Schoolager.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IFlashMessage _flashMessage;

        public StudentsController(
            IStudentRepository studentRepository,
            IConverterHelper converterHelper,
            IBlobHelper blobHelper,
            IUserHelper userHelper,
            IMailHelper mailHelper,
            IFlashMessage flashMessage)
        {
            _studentRepository = studentRepository;
            _converterHelper = converterHelper;
            _blobHelper = blobHelper;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _flashMessage = flashMessage;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await _studentRepository.GetAll().ToListAsync();
            return View(_studentRepository.GetAll());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentRepository.GetByIdAsync(id.Value);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            //ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Id");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = Guid.Empty;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "students");
                    }

                    var user = await _userHelper.GetUserByEmailAsync(model.Email);

                    if (user == null)
                    {
                        var student = _converterHelper.ToStudent(model, imageId, true);

                        user = _converterHelper.ToUser(student, new User(), "students");

                        var password = Guid.NewGuid().ToString();

                        var result = await _userHelper.AddUserAsync(user, password);

                        if(result != IdentityResult.Success)
                        {
                            throw new Exception("The user could not be created, please try again.");
                        }

                        // Add role or roles to user
                        await _userHelper.AddUserToRoleAsync(user, "Student");

                        // get the newly created user and set it as the students's user
                        student.User = await _userHelper.GetUserByEmailAsync(model.Email);

                        await _studentRepository.CreateAsync(student);

                        Response response = await ConfirmEmailAsync(user, model);

                        if (response.IsSuccess)
                        {
                            _flashMessage.Confirmation("The student has been created and confirmation email has been sent to user.");
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
            
            return View(model);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                // return new NotFoudnViewModel("StudentNotFound");
                return NotFound();
            }

            var student = await  _studentRepository.GetByIdAsync(id.Value);
            if (student == null)
            {
                // return new NotFoudnViewModel("StudentNotFound");
                return NotFound();
            }

            var model = _converterHelper.ToStudentViewModel(student);

            return View(model);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentViewModel model)
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
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "students");
                    }

                    var user = await _userHelper.GetUserByIdAsync(model.UserId);

                    if(user == null)
                    {
                        //return new NotFoundViewResult("StudentNotFound");
                        return NotFound();
                    }

                    var student = _converterHelper.ToStudent(model, imageId, false);

                    user = _converterHelper.ToUser(student, user, "students");

                    var response = await _userHelper.UpdateUserAsync(user);

                    if (response.Succeeded)
                    {
                        model.User = user;

                        student.UserId = user.Id;

                        await _studentRepository.UpdateAsync(student);

                        _flashMessage.Confirmation("Student has been updated.");

                        return View(model);
                    }

                    _flashMessage.Danger("An error ocurred whilst tryng to update the owner, please try again.");

                    return View(model);

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!await _studentRepository.ExistAsync(model.Id))
                    {
                        //return new NotFoundViewResult("StudentNotFound");
                        return NotFound();
                    }
                    else
                    {
                        _flashMessage.Danger(ex.Message);
                    }
                }
            }

            return View(model);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentRepository.GetByIdAsync(id.Value);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            await _studentRepository.DeleteAsync(student);
            return RedirectToAction(nameof(Index));
        }

        public async Task<Response> ConfirmEmailAsync(User user, StudentViewModel model)
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
