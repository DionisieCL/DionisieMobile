using Microsoft.AspNetCore.Http;
using Schoolager.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Schoolager.Web.Models.Teachers
{
    public class TeacherViewModel : Teacher
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
