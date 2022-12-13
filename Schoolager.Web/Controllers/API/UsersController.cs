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

namespace Schoolager.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserHelper _userHelper;


        public UsersController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }
        
        [HttpGet]
        [Route("GetAll")]
        public  IActionResult GetUsers()
        {
          
              var result =  _userHelper.GetAll();

                return Ok(result);
              
        }
        [HttpPost]
        [Route("GetUserByEmail")]
        public IActionResult Login(string email, string password)
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
    }
}