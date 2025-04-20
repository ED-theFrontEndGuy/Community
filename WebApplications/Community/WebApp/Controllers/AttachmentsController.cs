using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.DAL.EF;
using App.DAL.Interfaces;
using Base.Helpers;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AttachmentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAttachmentRepository _repository;
        private readonly IAssignmentRepository _assignmentRepository;

        public AttachmentsController(AppDbContext context, IAttachmentRepository repository, IAssignmentRepository assignmentRepository)
        {
            _context = context;
            _repository = repository;
            _assignmentRepository = assignmentRepository;
        }

        // GET: Attachments
        public async Task<IActionResult> Index()
        {
            var res = await _repository.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: Attachments/Details/5
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

        // GET: Attachments/Create
        public async Task<IActionResult> Create()
        {
            var assignments = await _assignmentRepository.AllAsync(User.GetUserId());
            
            var vm = new AttachmentCreateEditViewModel()
            {
                AssignmentSelectList = new SelectList(
                    assignments.Select(a => new {
                        Id = a.Id,
                        AssignmentName = a.Name
                    }),
                    "Id",        // This is the value field
                    "AssignmentName" // This is the display text
                )
                
            };
            
            return View(vm);
        }

        // POST: Attachments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttachmentCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(vm.Attachment);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: Attachments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachment = await _repository.FindAsync(id.Value, User.GetUserId());
            var assignments = await _assignmentRepository.AllAsync(User.GetUserId());
            
            if (attachment == null)
            {
                return NotFound();
            }
            
            var vm = new AttachmentCreateEditViewModel()
            {
                AssignmentSelectList = new SelectList(
                    assignments.Select(a => new {
                        Id = a.Id,
                        AssignmentName = a.Name
                    }),
                    "Id",        // This is the value field
                    "AssignmentName" // This is the display text
                ),
                Attachment = attachment
            };
            
            
            return View(vm);
        }

        // POST: Attachments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AttachmentCreateEditViewModel vm)
        {
            if (id != vm.Attachment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.Update(vm.Attachment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            
            var assignments = await _assignmentRepository.AllAsync(User.GetUserId());

            vm.AssignmentSelectList = new SelectList(
                assignments.Select(a => new
                {
                    Id = a.Id,
                    AssignmentName = a.Name
                }),
                "Id",
                "AssignmentName",
                vm.Attachment.Id);
            
            return View(vm);
        }

        // GET: Attachments/Delete/5
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

        // POST: Attachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _repository.RemoveAsync(id);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
