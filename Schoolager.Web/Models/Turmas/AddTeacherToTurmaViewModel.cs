using Schoolager.Web.Data.Entities;
using System.Collections.Generic;

namespace Schoolager.Web.Models.Turmas
{
    public class AddTeacherToTurmaViewModel
    {
        public List<SubjectTurmaViewModel> SubjectTurmaViewModels { get; set; }
        public int TurmaId { get; set; }
    }
}
