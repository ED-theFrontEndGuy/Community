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
    public class StudyGroupsController : Controller
    {
        private readonly IAppUOW _uow;

        public StudyGroupsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: StudyGroups
        public async Task<IActionResult> Index()
        {
            var res = await _uow.StudyGroupRepository.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: StudyGroups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.StudyGroupRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: StudyGroups/Create
        public async Task<IActionResult> Create()
        {
            var vm = new StudyGroupCreateEditViewModel()
            {
                StudySessionSelectList = new SelectList(await _uow.StudySessionRepository.AllAsync(User.GetUserId()),
                    nameof(StudySession.Id),
                    nameof(StudySession.Description))
            };
            
            return View(vm);
        }

        // POST: StudyGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudyGroupCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.StudyGroup.UserId = User.GetUserId();
                
                _uow.StudyGroupRepository.Add(vm.StudyGroup);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            vm.StudySessionSelectList = new SelectList(await _uow.StudySessionRepository.AllAsync(User.GetUserId()),
                nameof(StudySession.Id),
                nameof(StudySession.Description),
                vm.StudyGroup.Id);
            
            return View(vm);
        }

        // GET: StudyGroups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyGroup = await _uow.StudyGroupRepository.FindAsync(id.Value, User.GetUserId());
            
            if (studyGroup == null)
            {
                return NotFound();
            }

            var vm = new StudyGroupCreateEditViewModel()
            {
                StudySessionSelectList = new SelectList(await _uow.StudySessionRepository.AllAsync(User.GetUserId()),
                    nameof(StudySession.Id),
                    nameof(StudySession.Description)),
                StudyGroup = studyGroup
            };
            
            return View(vm);
        }

        // POST: StudyGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StudyGroupCreateEditViewModel vm)
        {
            if (id != vm.StudyGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vm.StudyGroup.UserId = User.GetUserId();
                
                _uow.StudyGroupRepository.Update(vm.StudyGroup);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.StudySessionSelectList = new SelectList(await _uow.StudySessionRepository.AllAsync(User.GetUserId()),
                nameof(StudySession.Id),
                nameof(StudySession.Description),
                vm.StudyGroup.Id);
            
            return View(vm);
        }

        // GET: StudyGroups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.StudyGroupRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: StudyGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.StudyGroupRepository.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
