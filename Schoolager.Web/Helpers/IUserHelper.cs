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

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task AddUserToRoleAsync(User user, string roleName);

        Task CheckRoleAsync(string roleName);
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<User> GetUserByIdAsync(string userId);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);

        Task<IdentityResult> UpdateUserAsync(User user);
        Task<bool> IsInRoleAsync(User user, string v);
        Task<string> GenerateChangeEmailTokenAsync(User user, string email);
        Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token);
        Task<IdentityResult> DeleteUserAsync(User user);
    }
}
