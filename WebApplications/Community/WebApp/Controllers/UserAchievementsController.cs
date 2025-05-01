using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.DAL.Interfaces;
using App.Domain;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class UserAchievementsController : Controller
    {
        private readonly IAppUOW _uow;

        public UserAchievementsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: UserAchievements
        public async Task<IActionResult> Index()
        {
            var res = await _uow.UserAchievementRepository.AllAsync(User.GetUserId()); 
                
            return View(res);
        }

        // GET: UserAchievements/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.UserAchievementRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: UserAchievements/Create
        public async Task<IActionResult> Create()
        {
            var vm = new UserAchievementsCreateEditViewModel()
            {
                AchievementSelectList = new SelectList(await _uow.AchievementRepository.AllAsync(User.GetUserId()),
                    nameof(Achievement.Id),
                    nameof(Achievement.Name))
            };
            
            return View(vm);
        }

        // POST: UserAchievements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserAchievementsCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.UserAchievement.UserId = User.GetUserId();
                _uow.UserAchievementRepository.Add(vm.UserAchievement);
                
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: UserAchievements/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAchievement = await _uow.UserAchievementRepository.FindAsync(id.Value, User.GetUserId());
            
            if (userAchievement == null)
            {
                return NotFound();
            }

            var vm = new UserAchievementsCreateEditViewModel()
            {
                AchievementSelectList = new SelectList(await _uow.AchievementRepository.AllAsync(User.GetUserId()),
                    nameof(Achievement.Id),
                    nameof(Achievement.Name),
                    userAchievement.Id),
                UserAchievement = userAchievement
            };
                
            return View(vm);
        }

        // POST: UserAchievements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserAchievementsCreateEditViewModel vm)
        {
            if (id != vm.UserAchievement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vm.UserAchievement.UserId = User.GetUserId();
                _uow.UserAchievementRepository.Update(vm.UserAchievement);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            vm.AchievementSelectList = new SelectList(await _uow.AchievementRepository.AllAsync(User.GetUserId()),
                nameof(Achievement.Id),
                nameof(Achievement.Name),
                vm.UserAchievement.Id);
            
            return View(vm);
        }

        // GET: UserAchievements/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.UserAchievementRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: UserAchievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.UserAchievementRepository.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
