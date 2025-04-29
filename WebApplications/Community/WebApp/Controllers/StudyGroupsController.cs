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
        private readonly IStudyGroupRepository _repository;
        private readonly IStudySessionRepository _studySessionRepository;

        public StudyGroupsController(IStudyGroupRepository repository, IStudySessionRepository studySessionRepository)
        {
            _repository = repository;
            _studySessionRepository = studySessionRepository;
        }

        // GET: StudyGroups
        public async Task<IActionResult> Index()
        {
            var res = await _repository.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: StudyGroups/Details/5
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

        // GET: StudyGroups/Create
        public async Task<IActionResult> Create()
        {
            var vm = new StudyGroupCreateEditViewModel()
            {
                StudySessionSelectList = new SelectList(await _studySessionRepository.AllAsync(User.GetUserId()),
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
                
                _repository.Add(vm.StudyGroup);
                await _repository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            vm.StudySessionSelectList = new SelectList(await _studySessionRepository.AllAsync(User.GetUserId()),
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

            var studyGroup = await _repository.FindAsync(id.Value, User.GetUserId());
            
            if (studyGroup == null)
            {
                return NotFound();
            }

            var vm = new StudyGroupCreateEditViewModel()
            {
                StudySessionSelectList = new SelectList(await _studySessionRepository.AllAsync(User.GetUserId()),
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
                
                _repository.Update(vm.StudyGroup);
                await _repository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.StudySessionSelectList = new SelectList(await _studySessionRepository.AllAsync(User.GetUserId()),
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

            var entity = await _repository.FindAsync(id.Value, User.GetUserId());
            
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
            await _repository.RemoveAsync(id);
            await _repository.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
