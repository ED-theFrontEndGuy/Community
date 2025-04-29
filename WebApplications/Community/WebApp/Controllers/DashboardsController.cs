using Microsoft.AspNetCore.Mvc;
using App.DAL.Interfaces;
using App.Domain;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class DashboardsController : Controller
    {
        private readonly IDashboardRepository _repository;

        public DashboardsController(IDashboardRepository repository)
        {
            _repository = repository;
        }

        // GET: Dashboards
        public async Task<IActionResult> Index()
        {
            var res = await _repository.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: Dashboards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _repository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Dashboards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dashboard dashboard)
        {
            dashboard.UserId = User.GetUserId();
            
            if (ModelState.IsValid)
            {
                _repository.Add(dashboard);
                await _repository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(dashboard);
        }

        // GET: Dashboards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboard = await _repository.FindAsync(id.Value, User.GetUserId());
            
            if (dashboard == null)
            {
                return NotFound();
            }
            
            return View(dashboard);
        }

        // POST: Dashboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Dashboard dashboard)
        {
            if (id != dashboard.Id)
            {
                return NotFound();
            }
            
            dashboard.UserId = User.GetUserId();

            if (ModelState.IsValid)
            {
                _repository.Update(dashboard);
                await _repository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(dashboard);
        }

        // GET: Dashboards/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _repository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: Dashboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _repository.RemoveAsync(id);
            await _repository.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
