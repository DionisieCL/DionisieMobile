using Schoolager.Web.Data.Entities;
using Schoolager.Web.Models.Students;
using System.Collections.Generic;

namespace Schoolager.Web.Models.Turmas
{
    public class AddStudentsViewModel
    {
        public List<StudentViewModel> FreeStudents { get; set; }
        public List<StudentViewModel> TurmaStudents { get; set; }
    }
}
