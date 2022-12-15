using System.ComponentModel.DataAnnotations;

namespace Schoolager.Web.Models.Account
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
