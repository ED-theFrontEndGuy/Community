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
    public class TimelogsController : Controller
    {
        private readonly ITimelogRepository _repository;
        private readonly IDeclarationRepository _declarationRepository;
        private readonly IAssignmentRepository _assignmentRepository;

        public TimelogsController(ITimelogRepository repository, IDeclarationRepository declarationRepository, IAssignmentRepository assignmentRepository)
        {
            _repository = repository;
            _declarationRepository = declarationRepository;
            _assignmentRepository = assignmentRepository;
        }

        // GET: Timelogs
        public async Task<IActionResult> Index()
        {
            var res = await _repository.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: Timelogs/Details/5
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

        // GET: Timelogs/Create
        public async Task<IActionResult> Create()
        {
            var assignments = await _assignmentRepository.AllAsync(User.GetUserId());
            var declarations = await _declarationRepository.AllAsync(User.GetUserId());

            var vm = new TimelogCreateEditViewModel()
            {
                AssignmentSelectList = new SelectList(assignments,
                    nameof(Assignment.Id),
                    nameof(Assignment.Name)),
                DeclarationSelectList = new SelectList(declarations.Select(
                        d => new 
                        { 
                            d.Id, 
                            CourseName = d.Course?.Name ?? "[No Course]" 
                        }), 
                    "Id",
                    "CourseName")
            };
            
            return View(vm);
        }

        // POST: Timelogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TimelogCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(vm.Timelog);
                await _repository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            var assignments = await _assignmentRepository.AllAsync(User.GetUserId());
            var declarations = await _declarationRepository.AllAsync(User.GetUserId());
            
            vm.AssignmentSelectList = new SelectList(assignments,
                nameof(Assignment.Id),
                nameof(Assignment.Name),
                vm.Timelog.AssignmentId);
            
            vm.DeclarationSelectList = new SelectList(declarations.Select(
                    d => new 
                    { 
                        d.Id, 
                        CourseName = d.Course?.Name ?? "[No Course]" 
                    }), 
                "Id",
                "CourseName",
                vm.Timelog.DeclarationId);
            
            return View(vm);
        }

        // GET: Timelogs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timelog = await _repository.FindAsync(id.Value, User.GetUserId());
            
            if (timelog == null)
            {
                return NotFound();
            }
            
            var assignments = await _assignmentRepository.AllAsync(User.GetUserId());
            var declarations = await _declarationRepository.AllAsync(User.GetUserId());

            var vm = new TimelogCreateEditViewModel()
            {
                DeclarationSelectList = new SelectList(declarations
                        .Select(
                            d => new
                            {
                                d.Id,
                                CourseName = d.Course?.Name ?? "[No Course]"
                            }),
                    "Id",
                    "CourseName",
                    timelog.DeclarationId),
                AssignmentSelectList = new SelectList(assignments,
                    nameof(Assignment.Id),
                    nameof(Assignment.Name),
                    timelog.AssignmentId),
                Timelog = timelog
            };
            
            return View(vm);
        }

        // POST: Timelogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TimelogCreateEditViewModel vm)
        {
            if (id != vm.Timelog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.Update(vm.Timelog);
                await _repository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            var assignments = await _assignmentRepository.AllAsync(User.GetUserId());
            var declarations = await _declarationRepository.AllAsync(User.GetUserId());

            vm.DeclarationSelectList = new SelectList(declarations.Select(
                    d => new
                    {
                        d.Id,
                        CourseName = d.Course?.Name ?? "[No Course]"
                    }),
                "Id",
                "CourseName",
                vm.Timelog.DeclarationId);
            
            vm.AssignmentSelectList = new SelectList(assignments,
                nameof(Assignment.Id),
                nameof(Assignment.Name),
                vm.Timelog.AssignmentId);
            
            return View(vm);
        }

        // GET: Timelogs/Delete/5
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

        // POST: Timelogs/Delete/5
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
