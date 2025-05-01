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
        private readonly IAppUOW _uow;

        public TimelogsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Timelogs
        public async Task<IActionResult> Index()
        {
            var res = await _uow.TimelogRepository.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: Timelogs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.TimelogRepository.FindAsync(id.Value, User.GetUserId()); 
                
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Timelogs/Create
        public async Task<IActionResult> Create()
        {
            var assignments = await _uow.AssignmentRepository.AllAsync(User.GetUserId());
            var declarations = await _uow.DeclarationRepository.AllAsync(User.GetUserId());

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
                _uow.TimelogRepository.Add(vm.Timelog);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            var assignments = await _uow.AssignmentRepository.AllAsync(User.GetUserId());
            var declarations = await _uow.DeclarationRepository.AllAsync(User.GetUserId());
            
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

            var timelog = await _uow.TimelogRepository.FindAsync(id.Value, User.GetUserId());
            
            if (timelog == null)
            {
                return NotFound();
            }
            
            var assignments = await _uow.AssignmentRepository.AllAsync(User.GetUserId());
            var declarations = await _uow.DeclarationRepository.AllAsync(User.GetUserId());

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
                _uow.TimelogRepository.Update(vm.Timelog);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            var assignments = await _uow.AssignmentRepository.AllAsync(User.GetUserId());
            var declarations = await _uow.DeclarationRepository.AllAsync(User.GetUserId());

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

            var entity = await _uow.TimelogRepository.FindAsync(id.Value, User.GetUserId());
            
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
            await _uow.TimelogRepository.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
