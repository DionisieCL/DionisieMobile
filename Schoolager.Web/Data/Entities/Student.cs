using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schoolager.Web.Data.Entities
{
    public class Student : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public Turma Turma { get; set; }
        public int? TurmaId { get; set; }

        public ICollection<Grade> Grades { get; set; }

        [Display(Name = "Image")]
        public Guid ImageUrl { get; set; }

        public User User { get; set; }

        public string ImageFullPath => ImageUrl == Guid.Empty
            ? $"https://schoolmanagesysstorage.blob.core.windows.net/noimage/noimage.png"
            : $"https://schoolmanagesysstorage.blob.core.windows.net/students/{ImageUrl}";
    }
}
