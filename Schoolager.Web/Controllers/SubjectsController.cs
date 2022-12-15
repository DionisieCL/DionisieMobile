using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Helpers;
using Vereyon.Web;

namespace Schoolager.Web.Controllers
{
    [Authorize(Roles = "Employee,Admin")]
    public class SubjectsController : Controller
    {
        private readonly DataContext _context;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IFlashMessage _flashMessage;

        public SubjectsController(DataContext context,
                                  ISubjectRepository subjectRepository,
                                  IFlashMessage flashMessage)
        {
            _context = context;
            _subjectRepository = subjectRepository;
            _flashMessage = flashMessage;
        }

        // GET: Subjects
        public IActionResult Index()
        {
            return View(_subjectRepository.GetAll());
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("SubjectNotFound");
            }

            var subject = await _subjectRepository.GetByIdAsync(id.Value);
            if (subject == null)
            {
                return new NotFoundViewResult("SubjectNotFound");
            }

            return View(subject);
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectRepository.CreateAsync(subject);
                _flashMessage.Confirmation("Subject Created.");
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("SubjectNotFound");
            }

            var subject = await _subjectRepository.GetByIdAsync(id.Value);

            if (subject == null)
            {
                return new NotFoundViewResult("SubjectNotFound");
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Subject subject)
        {
            if (id != subject.Id)
            {
                return new NotFoundViewResult("SubjectNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _subjectRepository.UpdateAsync(subject);
                    _flashMessage.Confirmation("Subject Updated.");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (! await _subjectRepository.ExistAsync(subject.Id))
                    {
                        return new NotFoundViewResult("SubjectNotFound");
                    }
                    else
                    {
                        _flashMessage.Danger(ex.Message);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("SubjectNotFound");
            }

            var subject = await _subjectRepository.GetByIdAsync(id.Value);
            if (subject == null)
            {
                return new NotFoundViewResult("SubjectNotFound");
            }

            try
            {
                await _subjectRepository.DeleteAsync(subject);
                _flashMessage.Confirmation("Subject Deleted.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"Something went wrong while trying to delete the subject {subject.Name}.";
                    ViewBag.ErrorMessage = $"The subject is already beeing used to assign a Grade please delete Grades associated with it first.</br></br>";
                }

                return View("Error");
            }
        }

        public IActionResult SubjectNotFound()
        {
            return View();
        }

    }
}
