﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Schoolager.Web.Data
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        private readonly DataContext _context;

        public SubjectRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboSubjects()
        {
            var list = _context.Subjects.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString(),
            }).OrderBy(sli => sli.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "< Select a subject >",
                Value = null,
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboSubjectsWithTurma(int id)
        {
            var list = _context.SubjectTurmas
                .Where(t => t.TurmaId == id)
                .Select(s => s.Subject)
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                }).OrderBy(sli => sli.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "< Select a subject >",
                Value = null,
            });

            return list;
        }
    }
}
