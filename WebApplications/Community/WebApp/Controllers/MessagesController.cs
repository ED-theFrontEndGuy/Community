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
    public class MessagesController : Controller
    {
        private readonly IMessageRepository _repository;
        private readonly IConversationRepository _conversationRepository;

        public MessagesController(IMessageRepository repository, IConversationRepository conversationRepository)
        {
            _repository = repository;
            _conversationRepository = conversationRepository;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var res = await _repository.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: Messages/Details/5
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

        // GET: Messages/Create
        public async Task<IActionResult> Create()
        {
            var vm = new MessageCreateEditViewModel()
            {
                ConversationSelectList = new SelectList(await _conversationRepository.AllAsync(User.GetUserId()),
                    nameof(Conversation.Id),
                    nameof(Conversation.Name)),
            };
            
            return View(vm);
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MessageCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Message.UserId = User.GetUserId();
                _repository.Add(vm.Message);
                
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _repository.FindAsync(id.Value, User.GetUserId());
            
            if (message == null)
            {
                return NotFound();
            }

            var vm = new MessageCreateEditViewModel()
            {
                ConversationSelectList = new SelectList(await _conversationRepository.AllAsync(User.GetUserId()),
                    nameof(Conversation.Id),
                    nameof(Conversation.Name)),
                Message = message
            };
            
            return View(vm);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MessageCreateEditViewModel vm)
        {
            if (id != vm.Message.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vm.Message.UserId = User.GetUserId();
                _repository.Update(vm.Message);
                await _repository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            vm.ConversationSelectList = new SelectList(await _conversationRepository.AllAsync(User.GetUserId()),
                nameof(Conversation.Id),
                nameof(Conversation.Name),
                vm.Message.Id);
            
            return View(vm);
        }

        // GET: Messages/Delete/5
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

        // POST: Messages/Delete/5
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
