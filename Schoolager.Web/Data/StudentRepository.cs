﻿using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {

        private readonly DataContext _context;

        public StudentRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<Student> GetStudentWithTurma(int id)
        //{
        //    return await _context.Students
        //        .Include(s => s.Turma)
        //        .Where(s => s.TurmaId == id)
        //        .FirstOrDefaultAsync();
        //}

        public async Task<List<Student>> GetStudentWithTurma(int id)
        {
            return await _context.Students
                .Where(s => s.TurmaId == id)
                .ToListAsync();
        }

    }
}
