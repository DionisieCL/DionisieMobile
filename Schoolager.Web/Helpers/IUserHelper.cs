using Microsoft.AspNetCore.Identity;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Models.Account;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Helpers
{
    public interface IUserHelper
    {
        IQueryable<User> GetAll();

        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
    }
}
