using Microsoft.AspNetCore.Mvc.Rendering;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;

namespace Schoolager.Web.Models.Turmas
{
    public class SubjectTurmaViewModel
    {
        public Subject Subject { get; set; }
        public IEnumerable<SelectListItem> Teachers { get; set; }
        public int TeacherId { get; set; }
    }
}
