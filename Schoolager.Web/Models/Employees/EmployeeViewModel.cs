using Microsoft.AspNetCore.Http;
using Schoolager.Web.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Schoolager.Web.Models.Employees
{
    public class EmployeeViewModel : User
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
