using Schoolager.Web.Data.Entities;
using System.Collections.Generic;

namespace Schoolager.Web.Models.Grades
{
    public class GradesViewModel
    {
        public int SubjectId { get; set; }
        public List<GradeViewModel> GradeViewModels { get; set; }
        public int TurmaId { get; set; }
    }
}
