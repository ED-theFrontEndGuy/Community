using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Controllers
{
    public class StudyGroupsController : Controller
    {
        private readonly AppDbContext _context;

        public StudyGroupsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StudyGroups
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StudyGroups.Include(s => s.StudySession);
            return View(await appDbContext.ToListAsync());
        }

        // GET: StudyGroups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyGroup = await _context.StudyGroups
                .Include(s => s.StudySession)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studyGroup == null)
            {
                return NotFound();
            }

            return View(studyGroup);
        }

        // GET: StudyGroups/Create
        public IActionResult Create()
        {
            ViewData["StudySessionId"] = new SelectList(_context.StudySessions, "Id", "Id");
            return View();
        }

        // POST: StudyGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudySessionId,Id")] StudyGroup studyGroup)
        {
            if (ModelState.IsValid)
            {
                studyGroup.Id = Guid.NewGuid();
                _context.Add(studyGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudySessionId"] = new SelectList(_context.StudySessions, "Id", "Id", studyGroup.StudySessionId);
            return View(studyGroup);
        }

        // GET: StudyGroups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyGroup = await _context.StudyGroups.FindAsync(id);
            if (studyGroup == null)
            {
                return NotFound();
            }
            ViewData["StudySessionId"] = new SelectList(_context.StudySessions, "Id", "Id", studyGroup.StudySessionId);
            return View(studyGroup);
        }

        // POST: StudyGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StudySessionId,Id")] StudyGroup studyGroup)
        {
            if (id != studyGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studyGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyGroupExists(studyGroup.Id))
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
            ViewData["StudySessionId"] = new SelectList(_context.StudySessions, "Id", "Id", studyGroup.StudySessionId);
            return View(studyGroup);
        }

        // GET: StudyGroups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyGroup = await _context.StudyGroups
                .Include(s => s.StudySession)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studyGroup == null)
            {
                return NotFound();
            }

            return View(studyGroup);
        }

        // POST: StudyGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var studyGroup = await _context.StudyGroups.FindAsync(id);
            if (studyGroup != null)
            {
                _context.StudyGroups.Remove(studyGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyGroupExists(Guid id)
        {
            return _context.StudyGroups.Any(e => e.Id == id);
        }
    }
}
