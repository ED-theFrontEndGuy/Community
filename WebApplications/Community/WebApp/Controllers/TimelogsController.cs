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
    public class TimelogsController : Controller
    {
        private readonly AppDbContext _context;

        public TimelogsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Timelogs
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Timelogs.Include(t => t.Assignment).Include(t => t.Declaration).Include(t => t.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Timelogs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timelog = await _context.Timelogs
                .Include(t => t.Assignment)
                .Include(t => t.Declaration)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timelog == null)
            {
                return NotFound();
            }

            return View(timelog);
        }

        // GET: Timelogs/Create
        public IActionResult Create()
        {
            ViewData["AssignmentId"] = new SelectList(_context.Assignments, "Id", "Name");
            ViewData["DeclarationId"] = new SelectList(_context.Declarations, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Timelogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartTime,EndTime,DeclarationId,UserId,AssignmentId,Id")] Timelog timelog)
        {
            if (ModelState.IsValid)
            {
                timelog.Id = Guid.NewGuid();
                _context.Add(timelog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignmentId"] = new SelectList(_context.Assignments, "Id", "Name", timelog.AssignmentId);
            ViewData["DeclarationId"] = new SelectList(_context.Declarations, "Id", "Id", timelog.DeclarationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", timelog.UserId);
            return View(timelog);
        }

        // GET: Timelogs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timelog = await _context.Timelogs.FindAsync(id);
            if (timelog == null)
            {
                return NotFound();
            }
            ViewData["AssignmentId"] = new SelectList(_context.Assignments, "Id", "Name", timelog.AssignmentId);
            ViewData["DeclarationId"] = new SelectList(_context.Declarations, "Id", "Id", timelog.DeclarationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", timelog.UserId);
            return View(timelog);
        }

        // POST: Timelogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StartTime,EndTime,DeclarationId,UserId,AssignmentId,Id")] Timelog timelog)
        {
            if (id != timelog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timelog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimelogExists(timelog.Id))
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
            ViewData["AssignmentId"] = new SelectList(_context.Assignments, "Id", "Name", timelog.AssignmentId);
            ViewData["DeclarationId"] = new SelectList(_context.Declarations, "Id", "Id", timelog.DeclarationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", timelog.UserId);
            return View(timelog);
        }

        // GET: Timelogs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timelog = await _context.Timelogs
                .Include(t => t.Assignment)
                .Include(t => t.Declaration)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timelog == null)
            {
                return NotFound();
            }

            return View(timelog);
        }

        // POST: Timelogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var timelog = await _context.Timelogs.FindAsync(id);
            if (timelog != null)
            {
                _context.Timelogs.Remove(timelog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimelogExists(Guid id)
        {
            return _context.Timelogs.Any(e => e.Id == id);
        }
    }
}
