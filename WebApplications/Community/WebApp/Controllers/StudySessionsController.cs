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
    public class StudySessionsController : Controller
    {
        private readonly AppDbContext _context;

        public StudySessionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StudySessions
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StudySessions.Include(s => s.Assignment).Include(s => s.Room).Include(s => s.StudyGroup);
            return View(await appDbContext.ToListAsync());
        }

        // GET: StudySessions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studySession = await _context.StudySessions
                .Include(s => s.Assignment)
                .Include(s => s.Room)
                .Include(s => s.StudyGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studySession == null)
            {
                return NotFound();
            }

            return View(studySession);
        }

        // GET: StudySessions/Create
        public IActionResult Create()
        {
            ViewData["AssignmentId"] = new SelectList(_context.Assignments, "Id", "Name");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name");
            ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups, "Id", "Id");
            return View();
        }

        // POST: StudySessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssignmentId,RoomId,StudyGroupId,Id")] StudySession studySession)
        {
            if (ModelState.IsValid)
            {
                studySession.Id = Guid.NewGuid();
                _context.Add(studySession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignmentId"] = new SelectList(_context.Assignments, "Id", "Name", studySession.AssignmentId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name", studySession.RoomId);
            ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups, "Id", "Id", studySession.StudyGroupId);
            return View(studySession);
        }

        // GET: StudySessions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studySession = await _context.StudySessions.FindAsync(id);
            if (studySession == null)
            {
                return NotFound();
            }
            ViewData["AssignmentId"] = new SelectList(_context.Assignments, "Id", "Name", studySession.AssignmentId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name", studySession.RoomId);
            ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups, "Id", "Id", studySession.StudyGroupId);
            return View(studySession);
        }

        // POST: StudySessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AssignmentId,RoomId,StudyGroupId,Id")] StudySession studySession)
        {
            if (id != studySession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studySession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudySessionExists(studySession.Id))
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
            ViewData["AssignmentId"] = new SelectList(_context.Assignments, "Id", "Name", studySession.AssignmentId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Name", studySession.RoomId);
            ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups, "Id", "Id", studySession.StudyGroupId);
            return View(studySession);
        }

        // GET: StudySessions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studySession = await _context.StudySessions
                .Include(s => s.Assignment)
                .Include(s => s.Room)
                .Include(s => s.StudyGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studySession == null)
            {
                return NotFound();
            }

            return View(studySession);
        }

        // POST: StudySessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var studySession = await _context.StudySessions.FindAsync(id);
            if (studySession != null)
            {
                _context.StudySessions.Remove(studySession);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudySessionExists(Guid id)
        {
            return _context.StudySessions.Any(e => e.Id == id);
        }
    }
}
