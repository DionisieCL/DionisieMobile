using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Models.Turmas;

namespace Schoolager.Web.Controllers
{
    public class TurmasController : Controller
    {
        private readonly DataContext _context;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISubjectTurmaRepository _subjectTurmaRepository;

        public TurmasController(DataContext context,
                                ISubjectRepository subjectRepository,
                                ISubjectTurmaRepository subjectTurmaRepository)
        {
            _context = context;
            _subjectRepository = subjectRepository;
            _subjectTurmaRepository = subjectTurmaRepository;
        }

        // GET: Turmas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Turma.ToListAsync());
        }

        // GET: Turmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _context.Turma
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turma == null)
            {
                return NotFound();
            }

            return View(turma);
        }

        // GET: Turmas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Turmas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Turma turma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turma);
        }

        // GET: Turmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _context.Turma.FindAsync(id);
            if (turma == null)
            {
                return NotFound();
            }
            return View(turma);
        }

        // POST: Turmas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Turma turma)
        {
            if (id != turma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurmaExists(turma.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(turma);
        }

        // GET: Turmas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _context.Turma
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turma == null)
            {
                return NotFound();
            }

            return View(turma);
        }

        // POST: Turmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turma = await _context.Turma.FindAsync(id);
            _context.Turma.Remove(turma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurmaExists(int id)
        {
            return _context.Turma.Any(e => e.Id == id);
        }

        //Teste adicionar Subjects a uma Turma
        public async Task<IActionResult> AddSubjectsToTurmaAsync(int? id)
        {

            var allSubjects = await _subjectRepository.GetAll().ToListAsync();

            var subjectsInTurma = await _subjectTurmaRepository.GetAllSubjectsWithTurma(id.Value);

            List<Subject> available = allSubjects.Where(st => !subjectsInTurma.Any(al => al.Id == st.Id)).ToList();

            var model = new AddSubjectsToTurmaViewModel
            {
                TurmaId = id.Value,
                AvailableSubjects = available,
                SubjectsInTurma = subjectsInTurma
            };

            ViewData["TurmaId"] = id.Value;
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> AddSubjects(List<Subject> subjects, int turmaId)
        {

            List<SubjectTurma> subTurmas = new List<SubjectTurma>();

            foreach (var item in subjects)
            {
                subTurmas.Add(new SubjectTurma
                {
                    SubjectId = item.Id,
                    TurmaId = turmaId
                });
            }

            try
            {
                await _subjectTurmaRepository.InsertSubjectTurmasAsync(subTurmas);
            }
            catch (Exception ec)
            {
                throw;
            }
            

            return Json(subjects);
        }

        [HttpPost]
        public async Task<JsonResult> RemoveSubjects(List<Subject> subjects, int turmaId)
        {

            List<SubjectTurma> subTurmas = new List<SubjectTurma>();

            foreach (var item in subjects)
            {
                subTurmas.Add(new SubjectTurma
                {
                    SubjectId = item.Id,
                    TurmaId = turmaId
                });
            }

            try
            {
                await _subjectTurmaRepository.RemoveSubjectTurmasAsync(subTurmas);
            }
            catch (Exception ec)
            {
                throw;
            }

            return Json(subjects);
        }

    }
}
