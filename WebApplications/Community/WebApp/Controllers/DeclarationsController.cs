using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.DAL.EF;
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
        private readonly AppDbContext _context;
        private readonly IDeclarationRepository _repository;
        private readonly ICourseRepository _courseRepository;

        public DeclarationsController(AppDbContext context, IDeclarationRepository repository, ICourseRepository courseRepository)
        {
            _context = context;
            _repository = repository;
            _courseRepository = courseRepository;
        }

        // GET: Declarations
        public async Task<IActionResult> Index()
        {
            var res = await _repository.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: Declarations/Details/5
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

        // GET: Declarations/Create
        public async Task<IActionResult> Create()
        {
            var vm = new DeclarationCreateEditViewModel()
            {
                CourseSelectList = new SelectList(await _courseRepository.AllAsync(User.GetUserId()),
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
                _repository.Add(vm.Declaration);
                
                await _context.SaveChangesAsync();
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

            var declaration = await _context.Declarations.FindAsync(id);
            if (declaration == null)
            {
                return NotFound();
            }

            var vm = new DeclarationCreateEditViewModel()
            {
                CourseSelectList = new SelectList(await _courseRepository.AllAsync(User.GetUserId()),
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
                
                _repository.Update(vm.Declaration);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            vm.CourseSelectList = new SelectList(await _courseRepository.AllAsync(User.GetUserId()),
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

            var entity = await _repository.FindAsync(id.Value, User.GetUserId());
                
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
            await _repository.RemoveAsync(id, User.GetUserId());
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
