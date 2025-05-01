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
    public class DeclarationsController : Controller
    {
        private readonly IAppUOW _uow;

        public DeclarationsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Declarations
        public async Task<IActionResult> Index()
        {
            var res = await _uow.DeclarationRepository.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: Declarations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.DeclarationRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Declarations/Create
        public async Task<IActionResult> Create()
        {
            var vm = new DeclarationCreateEditViewModel()
            {
                CourseSelectList = new SelectList(await _uow.CourseRepository.AllAsync(User.GetUserId()),
                    nameof(Course.Id),
                    nameof(Course.Name))
            };
            
            return View(vm);
        }

        // POST: Declarations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeclarationCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Declaration.UserId = User.GetUserId();
                _uow.DeclarationRepository.Add(vm.Declaration);
                
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: Declarations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var declaration = await _uow.DeclarationRepository.FindAsync(id.Value, User.GetUserId());
            
            if (declaration == null)
            {
                return NotFound();
            }

            var vm = new DeclarationCreateEditViewModel()
            {
                CourseSelectList = new SelectList(await _uow.CourseRepository.AllAsync(User.GetUserId()),
                    nameof(Course.Id),
                    nameof(Course.Name),
                    declaration.CourseId),
                Declaration = declaration
            };
            
            return View(vm);
        }

        // POST: Declarations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DeclarationCreateEditViewModel vm)
        {
            if (id != vm.Declaration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vm.Declaration.UserId = User.GetUserId();
                
                _uow.DeclarationRepository.Update(vm.Declaration);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            vm.CourseSelectList = new SelectList(await _uow.CourseRepository.AllAsync(User.GetUserId()),
                nameof(Course.Id),
                nameof(Course.Name),
                vm.Declaration.CourseId);
            
            return View(vm);
        }

        // GET: Declarations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.DeclarationRepository.FindAsync(id.Value, User.GetUserId());
                
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: Declarations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.DeclarationRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
