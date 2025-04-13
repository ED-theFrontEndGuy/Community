using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.DAL.Interfaces;
using App.Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class AchievementsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAchievementRepository _repository;

        public AchievementsController(AppDbContext context, IAchievementRepository repository)
        {
            _context = context;
            _repository = repository;
        }
        
        // GET: Achievements
        public async Task<IActionResult> Index()
        {
            return View(await _repository.AllAsync());
        }

        // GET: Achievements/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _repository.FindAsync(id.Value);

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Achievements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Achievements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Achievement entity)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(entity);
        }

        // GET: Achievements/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievement = await _context.Achievements.FindAsync(id);
            if (achievement == null)
            {
                return NotFound();
            }
            return View(achievement);
        }

        // POST: Achievements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Achievement achievement)
        {
            if (id != achievement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.Update(achievement);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(achievement);
        }

        // GET: Achievements/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievement = await _context.Achievements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achievement == null)
            {
                return NotFound();
            }

            return View(achievement);
        }

        // POST: Achievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _repository.RemoveAsync(id);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool AchievementExists(Guid id)
        {
            return _context.Achievements.Any(e => e.Id == id);
        }
    }
}
