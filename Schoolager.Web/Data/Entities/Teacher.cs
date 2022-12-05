using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schoolager.Web.Data.Entities
{
    public class Teacher : IEntity, IIsUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter a first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter a last name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You must enter an email address")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public Subject Subject { get; set; }

        [Display(Name = "Subject")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Subject")]
        public int SubjectId { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }

        public ICollection<TeacherTurma> TeacherTurmas{ get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://schoolmanagesysstorage.blob.core.windows.net/noimage/noimage.png"
            : $"https://schoolmanagesysstorage.blob.core.windows.net/students/{ImageId}";

        public string FullName => $"{FirstName} {LastName}";
    }
}
