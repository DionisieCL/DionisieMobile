using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Helpers;
using Schoolager.Web.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace Schoolager.Web.Controllers
{
    public class EmployeesController : Controller
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IFlashMessage _flashMessage;

        public EmployeesController(
            IStudentRepository studentRepository,
            IConverterHelper converterHelper,
            IBlobHelper blobHelper,
            IUserHelper userHelper,
            IMailHelper mailHelper,
            IFlashMessage flashMessage,
            DataContext context)
        {
            _studentRepository = studentRepository;
            _converterHelper = converterHelper;
            _blobHelper = blobHelper;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _flashMessage = flashMessage;
        }


        public async Task<IActionResult> Index()
        {
            var users = _userHelper.GetAll();

            List<User> usersInRole = new List<User>();

            foreach (var user in users)
            {
                if (await _userHelper.IsInRoleAsync(user,"Employee"))
                {
                    usersInRole.Add(user);
                }
            }

            return View(usersInRole);
        }

        public IActionResult Create()
        {
            var model = new EmployeeViewModel
            {

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = Guid.Empty;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "employees");
                    }

                    var user = await _userHelper.GetUserByEmailAsync(model.Email);

                    if (user == null)
                    {

                        user = new User
                        {
                            Id = model.Id,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            ImageId = imageId,
                            UserName = model.Email,
                            IsAdmin = model.IsAdmin,
                            BlobContainer = "employees"
                        };

                        var password = Guid.NewGuid().ToString();

                        var result = await _userHelper.AddUserAsync(user, password);

                        if (result != IdentityResult.Success)
                        {
                            throw new Exception("The user could not be created, please try again.");
                        }

                        if (model.IsAdmin == true)
                        {
                            await _userHelper.AddUserToRoleAsync(user, "Admin");
                        }

                        await _userHelper.AddUserToRoleAsync(user, "Employee");

                        
                        Response response = await ConfirmEmailAsync(user, model);

                        if (response.IsSuccess)
                        {
                            _flashMessage.Confirmation("The employee has been created and confirmation email has been sent to user.");
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


        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("EmployeeNotFound");
            }

            var user = await _userHelper.GetUserByIdAsync(id);

            if (user == null)
            {
                return new NotFoundViewResult("EmployeeNotFound");
            }

            return View(user);
        }


        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                // return new NotFoudnViewModel("StudentNotFound");
                return new NotFoundViewResult("EmployeeNotFound");
            }

            var employee = await _userHelper.GetUserByIdAsync(id);

            if (employee == null)
            {
                // return new NotFoudnViewModel("StudentNotFound");
                return new NotFoundViewResult("EmployeeNotFound");
            }

            var model = _converterHelper.ToEmployeeViewModel(employee);

            return View(model);
        }




        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EmployeeViewModel model)
        {
            if (id != model.Id)
            {
                return new NotFoundViewResult("EmployeeNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "employees");
                    }

                    var user = await _userHelper.GetUserByIdAsync(model.Id);

                    if (user == null)
                    {
                        //return new NotFoundViewResult("EmployeeNotFound");
                        return new NotFoundViewResult("EmployeeNotFound");
                    }

                    string email = user.Email;

                    if (email != model.Email)
                    {
                        user.EmailConfirmed = false;
                    }

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = email;
                    user.ImageId = imageId;
                    user.PhoneNumber = model.PhoneNumber;
                    user.IsAdmin = model.IsAdmin;
                    user.BlobContainer = "employees";

                    var response = await _userHelper.UpdateUserAsync(user);

                    if (response.Succeeded)
                    {

                        if(email != model.Email)
                        {
                            Response emailResponse = await SendConfirmNewEmailAsync(user, user.Email);

                            if (emailResponse.IsSuccess)
                            {
                                _flashMessage.Confirmation("The email to confirm the new username has been sent.");
                            }
                        }

                        _flashMessage.Confirmation("Employee has been updated.");

                        return View(model);
                    }

                    _flashMessage.Danger("An error ocurred whilst tryng to update the Employee, please try again.");

                    return View(model);

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var user = _userHelper.GetUserByIdAsync(model.Id);
                    if (user == null)
                    {
                        //return new NotFoundViewResult("StudentNotFound");
                        return new NotFoundViewResult("EmployeeNotFound");
                    }
                    else
                    {
                        _flashMessage.Danger(ex.Message);
                    }
                }
            }

            return View(model);
        }


        public async Task<IActionResult> Delete(string id)
        {

            if (id == null)
            {
                return new NotFoundViewResult("EmployeeNotFound");
            }

            var user = await _userHelper.GetUserByIdAsync(id);

            if (user == null)
            {
                return new NotFoundViewResult("EmployeeNotFound");
            }

            try
            {
                await _userHelper.DeleteUserAsync(user);

                _flashMessage.Confirmation("Employee deleted successfully");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"Something went wrong while trying to delete {user.FullName}";
                    ViewBag.ErrorMessage = $"Please try again in a few moments or contact the system administrators.</br></br>";
                }

                return View("Error");
            }
        }


        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var employee = await _userHelper.GetUserByIdAsync(id);

        //}
        public IActionResult EmployeeNotFound()
        {
            return View();
        }

        public async Task<Response> ConfirmEmailAsync(User user, EmployeeViewModel model)
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

        private async Task<Response> SendConfirmNewEmailAsync(User user, string newEmail)
        {
            var myToken = await _userHelper.GenerateChangeEmailTokenAsync(user, newEmail);

            var link = this.Url.Action(
                "ConfirmNewEmail",
                "Account",
                new
                {
                    userId = user.Id,
                    newEmail = newEmail,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

            Response response = _mailHelper.SendEmail(newEmail,
                "Confirm New Email",
                $"<h1>Changed email</h1>" +
            $"To confirm your new email click in this link:</br></br>" +
            $"<a href = \"{link}\">Confirm new email</a>");

            return response;
        }

    }
}
