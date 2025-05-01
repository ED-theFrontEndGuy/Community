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
    public class StudySessionsController : Controller
    {
        private readonly IAppUOW _uow;

        public StudySessionsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: StudySessions
        public async Task<IActionResult> Index()
        {
            var res = await _uow.StudySessionRepository.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: StudySessions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.StudySessionRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: StudySessions/Create
        public async Task<IActionResult> Create()
        {
            var assignments = await _uow.AssignmentRepository.AllAsync(User.GetUserId());
            var rooms = await _uow.RoomRepository.AllAsync(User.GetUserId());

            var vm = new StudySessionCreateEditViewModel()
            {
                AssignmentSelectList = new SelectList(assignments,
                    nameof(Assignment.Id),
                    nameof(Assignment.Name)),
                RoomSelectList = new SelectList(rooms,
                    nameof(Room.Id),
                    nameof(Room.Name))
            };
            
            return View(vm);
        }

        // POST: StudySessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudySessionCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.StudySessionRepository.Add(vm.StudySession);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            var assignments = await _uow.AssignmentRepository.AllAsync(User.GetUserId());
            var rooms = await _uow.RoomRepository.AllAsync(User.GetUserId());
            
            vm.AssignmentSelectList = new SelectList(assignments,
                nameof(Assignment.Id),
                nameof(Assignment.Name),
                vm.StudySession.AssignmentId);
            
            vm.RoomSelectList = new SelectList(rooms,
                nameof(Room.Id),
                nameof(Room.Name),
                vm.StudySession.RoomId);
            
            return View(vm);
        }

        // GET: StudySessions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studySession = await _uow.StudySessionRepository.FindAsync(id.Value, User.GetUserId());
            
            if (studySession == null)
            {
                return NotFound();
            }

            var vm = new StudySessionCreateEditViewModel()
            {
                AssignmentSelectList = new SelectList(await _uow.AssignmentRepository.AllAsync(User.GetUserId()),
                    nameof(Assignment.Id),
                    nameof(Assignment.Name),
                    studySession.AssignmentId),
                RoomSelectList = new SelectList(await _uow.RoomRepository.AllAsync(User.GetUserId()),
                    nameof(Room.Id),
                    nameof(Room.Name),
                    studySession.RoomId),
                StudySession = studySession
            };
            
            return View(vm);
        }

        // POST: StudySessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StudySessionCreateEditViewModel vm)
        {
            if (id != vm.StudySession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.StudySessionRepository.Update(vm.StudySession);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.AssignmentSelectList = new SelectList(await _uow.AssignmentRepository.AllAsync(User.GetUserId()),
                nameof(Assignment.Id),
                nameof(Assignment.Name),
                vm.StudySession.AssignmentId);
            vm.RoomSelectList = new SelectList(await _uow.RoomRepository.AllAsync(User.GetUserId()),
                nameof(Room.Id),
                nameof(Room.Name),
                vm.StudySession.RoomId);
            
            return View(vm);
        }

        // GET: StudySessions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.StudySessionRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: StudySessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.StudySessionRepository.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
