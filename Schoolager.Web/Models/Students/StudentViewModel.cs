using Microsoft.AspNetCore.Http;
using Schoolager.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Schoolager.Web.Models.Students
{
    public class StudentViewModel : Student
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
