using Microsoft.AspNetCore.Mvc;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Helpers;
using Schoolager.Web.Models.Account;
using System;
using System.Linq;
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
                    var user = _userHelper.GetUserByEmailAsync(model.Username);

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

        public IActionResult ConfirmEmail(string userId, string confirmationToken, string passwordToken)
        {
            if (string.IsNullOrEmpty(userId) ||
                string.IsNullOrEmpty(confirmationToken) ||
                string.IsNullOrEmpty(passwordToken))
            {
                //return RedirectToAction(nameof(NotAuthorized));
                return NotFound();
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
                    return NotFound();
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

        public IActionResult Test()
        {
            return View();
        }
    }
}
