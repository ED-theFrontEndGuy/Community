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
    public class UserAchievementsController : Controller
    {
        private readonly AppDbContext _context;

        public UserAchievementsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserAchievements
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserAchievements.Include(u => u.Achievement).Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserAchievements/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAchievement = await _context.UserAchievements
                .Include(u => u.Achievement)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAchievement == null)
            {
                return NotFound();
            }

            return View(userAchievement);
        }

        // GET: UserAchievements/Create
        public IActionResult Create()
        {
            ViewData["AchievementId"] = new SelectList(_context.Achievements, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id");
            return View();
        }

        // POST: UserAchievements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AchievementId,UserId,Id")] UserAchievement userAchievement)
        {
            if (ModelState.IsValid)
            {
                userAchievement.Id = Guid.NewGuid();
                _context.Add(userAchievement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AchievementId"] = new SelectList(_context.Achievements, "Id", "Name", userAchievement.AchievementId);
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", userAchievement.UserId);
            return View(userAchievement);
        }

        // GET: UserAchievements/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAchievement = await _context.UserAchievements.FindAsync(id);
            if (userAchievement == null)
            {
                return NotFound();
            }
            ViewData["AchievementId"] = new SelectList(_context.Achievements, "Id", "Name", userAchievement.AchievementId);
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", userAchievement.UserId);
            return View(userAchievement);
        }

        // POST: UserAchievements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AchievementId,UserId,Id")] UserAchievement userAchievement)
        {
            if (id != userAchievement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAchievement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAchievementExists(userAchievement.Id))
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
            ViewData["AchievementId"] = new SelectList(_context.Achievements, "Id", "Name", userAchievement.AchievementId);
            ViewData["UserId"] = new SelectList(_context.AppUsers, "Id", "Id", userAchievement.UserId);
            return View(userAchievement);
        }

        // GET: UserAchievements/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAchievement = await _context.UserAchievements
                .Include(u => u.Achievement)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAchievement == null)
            {
                return NotFound();
            }

            return View(userAchievement);
        }

        // POST: UserAchievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userAchievement = await _context.UserAchievements.FindAsync(id);
            if (userAchievement != null)
            {
                _context.UserAchievements.Remove(userAchievement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAchievementExists(Guid id)
        {
            return _context.UserAchievements.Any(e => e.Id == id);
        }
    }
}
