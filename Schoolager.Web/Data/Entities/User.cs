using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Schoolager.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters length.")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters length.")]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string FullName => $"{FirstName} {LastName}";

        // Keep record if user has changed its password before the first login
        public bool PasswordChanged { get; set; }

        public string BlobContainer { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
             ? $"https://vetmanage.azurewebsites.net/images/noimage.png"
             : $"https://vetmanagestorage.blob.core.windows.net/{BlobContainer}/{ImageId}";
    }
}
