using Schoolager.Web.Data.Entities;
using System.Collections.Generic;

namespace Schoolager.Web.Models.Turmas
{
    public class AddSubjectsToTurmaViewModel
    {
        public int TurmaId { get; set; }
        public List<Subject> AvailableSubjects {get; set;}
        public List<Subject> SubjectsInTurma { get; set; }
    }
}
