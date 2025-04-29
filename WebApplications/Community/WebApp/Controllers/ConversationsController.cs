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
    public class ConversationsController : Controller
    {
        private readonly IConversationRepository _repository;
        private readonly IStudyGroupRepository _studyGroupRepository;

        public ConversationsController(IConversationRepository repository, IStudyGroupRepository studyGroupRepository)
        {
            _repository = repository;
            _studyGroupRepository = studyGroupRepository;
        }

        // GET: Conversations
        public async Task<IActionResult> Index()
        {
            var res = await _repository.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: Conversations/Details/5
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

        // GET: Conversations/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ConversationCreateEditViewModel()
            {
                StudyGroupSelectList = new SelectList(await _studyGroupRepository.AllAsync(User.GetUserId()),
                    nameof(StudyGroup.Id),
                    nameof(StudyGroup.Name))
            };
            
            return View(vm);
        }

        // POST: Conversations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConversationCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(vm.Conversation);
                await _repository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.StudyGroupSelectList = new SelectList(await _studyGroupRepository.AllAsync(User.GetUserId()),
                nameof(StudyGroup.Id),
                nameof(StudyGroup.Name),
                vm.Conversation.Id);
            
            return View(vm);
        }

        // GET: Conversations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _repository.FindAsync(id.Value, User.GetUserId());
            
            if (conversation == null)
            {
                return NotFound();
            }

            var vm = new ConversationCreateEditViewModel()
            {
                Conversation = conversation,
                StudyGroupSelectList = new SelectList(await _studyGroupRepository.AllAsync(User.GetUserId()),
                    nameof(StudyGroup.Id),
                    nameof(StudyGroup.Name))
            };
            
            return View(vm);
        }

        // POST: Conversations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ConversationCreateEditViewModel vm)
        {
            if (id != vm.Conversation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.Update(vm.Conversation);
                await _repository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.StudyGroupSelectList = new SelectList(await _studyGroupRepository.AllAsync(User.GetUserId()),
                nameof(StudyGroup.Id),
                nameof(StudyGroup.Name),
                vm.Conversation.Id);
            
            return View(vm);
        }

        // GET: Conversations/Delete/5
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

        // POST: Conversations/Delete/5
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
