using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Helpers;
using Schoolager.Web.Models.Account;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vereyon.Web;

namespace Schoolager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IFlashMessage _flashMessage;

        public AccountController(
            IUserHelper userHelper,
            IFlashMessage flashMessage)
        {
            _userHelper = userHelper;
            _flashMessage = flashMessage;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Teacher") || User.IsInRole("Student"))
                {
                    return RedirectToAction("TeacherIndex", "Lessons");
                } 
                else if (User.IsInRole("Employee") || User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Turmas");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);

                if (result.Succeeded)
                {
                    var user = await _userHelper.GetUserByEmailAsync(model.Username);

                    if(await _userHelper.IsInRoleAsync(user, "Teacher") ||
                        await _userHelper.IsInRoleAsync(user, "Student"))
                    {
                        return RedirectToAction("TeacherIndex", "Lessons");
                    } 
                    else if(await _userHelper.IsInRoleAsync(user, "Employee") ||
                        await _userHelper.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Turmas");
                    }

                    return RedirectToAction(nameof(Test));
                    //if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    //{
                    //    return Redirect(this.Request.Query["ReturnUrl"].First());
                    //}
                    //return this.RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Failed to Login");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();

            return RedirectToAction(nameof(Login));
        }

        //[Authorize]
        public IActionResult ConfirmEmail(string userId, string confirmationToken, string passwordToken)
        {
            if (string.IsNullOrEmpty(userId) ||
                string.IsNullOrEmpty(confirmationToken) ||
                string.IsNullOrEmpty(passwordToken))
            {
                //return RedirectToAction(nameof(NotAuthorized));
                return new NotFoundViewResult("UserNotFound");
            }

            var model = new ConfirmAccountViewModel()
            {
                UserId = userId,
                ConfirmationToken = confirmationToken,
                PasswordToken = passwordToken,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(ConfirmAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByIdAsync(model.UserId);

                if (user == null)
                {
                    //return new NotFoundViewResult("UserNotFound");
                    return new NotFoundViewResult("UserNotFound");
                }

                var result = await _userHelper.ConfirmEmailAsync(user, model.ConfirmationToken);

                if (!result.Succeeded)
                {
                    _flashMessage.Danger("There was a problem confirming your account, please try again.");

                    return View(model);
                }

                var result2 = await _userHelper.ResetPasswordAsync(user, model.PasswordToken, model.Password);

                if (result2.Succeeded)
                {
                    try
                    {
                        // User has changed its password after its account was first created
                        user.PasswordChanged = true;

                        // Update the user 
                        var response = await _userHelper.UpdateUserAsync(user);

                        if (response.Succeeded)
                        {
                            _flashMessage.Confirmation("Password changed and account confirmed. You may now login.");

                            return RedirectToAction(nameof(Login));
                        }
                    }
                    catch (Exception ex)
                    {
                        _flashMessage.Danger(ex.Message);
                    }
                }
            }
            _flashMessage.Danger("There was a problem confirming your account, please try again.");

            return View(model);
        }

        public IActionResult RecoverPassword()
        {
            return View();
        }


        public IActionResult NotAuthorized()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmNewEmail(string userId, string newEmail, string token)
        {
            if (string.IsNullOrEmpty(userId) ||
                string.IsNullOrEmpty(newEmail) ||
                string.IsNullOrEmpty(token))
            {
                return new NotFoundViewResult("UserNotFound");
            }
            var user = await _userHelper.GetUserByIdAsync(userId);

            if (user != null)
            {
                var result = await _userHelper.ChangeEmailAsync(user, newEmail, token);

                if (result.Succeeded)
                {
                    user.UserName = newEmail;

                    await _userHelper.UpdateUserAsync(user);

                    ViewData["Message"] = "Your new email was confirmed.";
                    ViewData["Success"] = true;

                    return View();
                }
            }
            ViewData["Message"] = "There was a problem confirming your new email, please try again.";
            ViewData["Success"] = false;
            return View();
        }

        [HttpPost]
        [Route("Account/GetUserAsync")]
        public async Task<JsonResult> GetUserAsync(int countryId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userHelper.GetUserByIdAsync(userId);

            if (user != null)
            {
                return Json(user);
            }
            return Json("NotAuthorized");
        }

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult UserNotFound()
        {
            return View();
        }
    }
}
