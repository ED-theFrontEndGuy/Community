using App.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Domain;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class DeclarationsController : Controller
    {
        private readonly IAppBLL _bll;

        public DeclarationsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Declarations
        public async Task<IActionResult> Index()
        {
            var res = await _bll.DeclarationService.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: Declarations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _bll.DeclarationService.FindAsync(id.Value, User.GetUserId());
            
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
                CourseSelectList = new SelectList(await _bll.CourseService.AllAsync(User.GetUserId()),
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
                _bll.DeclarationService.Add(vm.Declaration, User.GetUserId());
                
                await _bll.SaveChangesAsync();
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

            var declaration = await _bll.DeclarationService.FindAsync(id.Value, User.GetUserId());
            
            if (declaration == null)
            {
                return NotFound();
            }

            var vm = new DeclarationCreateEditViewModel()
            {
                CourseSelectList = new SelectList(await _bll.CourseService.AllAsync(User.GetUserId()),
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
                _bll.DeclarationService.Update(vm.Declaration);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            vm.CourseSelectList = new SelectList(await _bll.CourseService.AllAsync(User.GetUserId()),
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

            var entity = await _bll.DeclarationService.FindAsync(id.Value, User.GetUserId());
                
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
            await _bll.DeclarationService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
