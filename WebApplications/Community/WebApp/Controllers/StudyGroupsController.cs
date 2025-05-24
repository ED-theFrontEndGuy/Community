using App.BLL.Interfaces;
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
        private readonly IAppBLL _bll;

        public StudyGroupsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: StudyGroups
        public async Task<IActionResult> Index()
        {
            var res = await _bll.StudyGroupService.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: StudyGroups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _bll.StudyGroupService.FindAsync(id.Value, User.GetUserId());
            
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
                StudySessionSelectList = new SelectList(await _bll.StudySessionService.AllAsync(User.GetUserId()),
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
                _bll.StudyGroupService.Add(vm.StudyGroup, User.GetUserId());
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            vm.StudySessionSelectList = new SelectList(await _bll.StudySessionService.AllAsync(User.GetUserId()),
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

            var studyGroup = await _bll.StudyGroupService.FindAsync(id.Value, User.GetUserId());
            
            if (studyGroup == null)
            {
                return NotFound();
            }

            var vm = new StudyGroupCreateEditViewModel()
            {
                StudySessionSelectList = new SelectList(await _bll.StudySessionService.AllAsync(User.GetUserId()),
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
                _bll.StudyGroupService.Update(vm.StudyGroup);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.StudySessionSelectList = new SelectList(await _bll.StudySessionService.AllAsync(User.GetUserId()),
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

            var entity = await _bll.StudyGroupService.FindAsync(id.Value, User.GetUserId());
            
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
            await _bll.StudyGroupService.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
