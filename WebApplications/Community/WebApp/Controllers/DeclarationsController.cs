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
    public class DeclarationsController : Controller
    {
        private readonly AppDbContext _context;

        public DeclarationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Declarations
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Declarations.Include(d => d.Course).Include(d => d.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Declarations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var declaration = await _context.Declarations
                .Include(d => d.Course)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (declaration == null)
            {
                return NotFound();
            }

            return View(declaration);
        }

        // GET: Declarations/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Declarations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Active,UserId,CourseId,Id")] Declaration declaration)
        {
            if (ModelState.IsValid)
            {
                declaration.Id = Guid.NewGuid();
                _context.Add(declaration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", declaration.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", declaration.UserId);
            return View(declaration);
        }

        // GET: Declarations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var declaration = await _context.Declarations.FindAsync(id);
            if (declaration == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", declaration.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", declaration.UserId);
            return View(declaration);
        }

        // POST: Declarations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Active,UserId,CourseId,Id")] Declaration declaration)
        {
            if (id != declaration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(declaration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeclarationExists(declaration.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", declaration.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", declaration.UserId);
            return View(declaration);
        }

        // GET: Declarations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var declaration = await _context.Declarations
                .Include(d => d.Course)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (declaration == null)
            {
                return NotFound();
            }

            return View(declaration);
        }

        // POST: Declarations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var declaration = await _context.Declarations.FindAsync(id);
            if (declaration != null)
            {
                _context.Declarations.Remove(declaration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeclarationExists(Guid id)
        {
            return _context.Declarations.Any(e => e.Id == id);
        }
    }
}
