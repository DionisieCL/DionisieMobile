using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schoolager.Web.Data.Entities
{
    public class Student : IEntity, IIsUser
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

        public Turma Turma { get; set; }

        public int? TurmaId { get; set; }

        public int SchoolYear { get; set; }

        public ICollection<Grade> Grades { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://schoolmanagesysstorage.blob.core.windows.net/noimage/noimage.png"
            : $"https://schoolmanagesysstorage.blob.core.windows.net/students/{ImageId}";

        public ICollection<StudentLessonData> StudentLessonsData { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
