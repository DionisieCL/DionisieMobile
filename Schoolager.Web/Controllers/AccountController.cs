using Microsoft.AspNetCore.Mvc;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Helpers;
using Schoolager.Web.Models.Account;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;

        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
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

        public IActionResult Test()
        {
            return View();
        }
    }
}
