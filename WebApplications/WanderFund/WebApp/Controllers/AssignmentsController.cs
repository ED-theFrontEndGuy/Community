using App.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class AssignmentsController : Controller
    {
        private readonly IAppBLL _bll;
        
        public AssignmentsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            var res = await _bll.AssignmentService.AllAsync(User.GetUserId());
            return View(res);
        }

        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _bll.AssignmentService.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Assignments/Create
        public async Task<IActionResult> Create()
        {
            var declarations = await _bll.DeclarationService.AllAsync(User.GetUserId());
            
            var vm = new AssignmentCreateEditViewModel
            {
                DeclarationSelectList = new SelectList(
                    declarations.Select(d => new {
                        Id = d.Id,
                        CourseName = d.Course?.Name ?? "Unknown" // Safeguard if Name is null
                    }),
                    "Id",        // This is the value field
                    "CourseName" // This is the display text
                )
            };


            
            return View(vm);
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssignmentCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.AssignmentService.Add(vm.Assignment, User.GetUserId());
                
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _bll.AssignmentService.FindAsync(id.Value, User.GetUserId());
            var declarations = await _bll.DeclarationService.AllAsync(User.GetUserId());
            
            if (assignment == null)
            {
                return NotFound();
            }
            
            
            var vm = new AssignmentCreateEditViewModel
            {
                DeclarationSelectList = new SelectList(
                    declarations.Select(d => new {
                        Id = d.Id,
                        CourseName = d.Course!.Name ?? "Unknown"
                    }),
                    "Id",        // This is the value field
                    "CourseName" // This is the display text
                ),
                Assignment = assignment
            };
            
            
            return View(vm);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,AssignmentCreateEditViewModel vm)
        {
            if (id != vm.Assignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.AssignmentService.Update(vm.Assignment);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            var declarations = await _bll.DeclarationService.AllAsync(User.GetUserId());
            
            vm.DeclarationSelectList = new SelectList(
                declarations.Select(d => new {
                    Id = d.Id,
                    CourseName = d.Course!.Name ?? "Unknown"
                }),
                "Id",        // This is the value field
                "CourseName", // This is the display text
            vm.Assignment.Id);
            
            return View(vm);
        }

        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var entity = await _bll.AssignmentService.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.AssignmentService.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
