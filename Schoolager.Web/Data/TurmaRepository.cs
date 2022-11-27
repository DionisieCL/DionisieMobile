﻿using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using System.Linq;

namespace Schoolager.Web.Data
{
    public class TurmaRepository : GenericRepository<Turma>, ITurmaRepository
    {
        private readonly DataContext _context;

        public TurmaRepository(DataContext context) : base(context)
        {
            _context = context;

            _context.Students
                .Where(s => s.Turma.Id == 1)
                .Include(s => s.Grades.Where(g => g.SubjectId == 1));
        }
    }
}
