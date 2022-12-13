using Microsoft.AspNetCore.Http;
using Schoolager.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Schoolager.Web.Models.Lessons
{
    public class LessonResourceViewModel : LessonResource
    {
        [Required]
        public IFormFile FormFile { get; set; }
    }
}
