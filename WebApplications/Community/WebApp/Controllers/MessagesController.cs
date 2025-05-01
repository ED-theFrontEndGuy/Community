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
        private readonly IAppUOW _uow;

        public MessagesController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var res = await _uow.MessageRepository.AllAsync(User.GetUserId());
            
            return View(res);
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.MessageRepository.FindAsync(id.Value, User.GetUserId());
            
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
                ConversationSelectList = new SelectList(await _uow.ConversationRepository.AllAsync(User.GetUserId()),
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
                _uow.MessageRepository.Add(vm.Message);
                
                await _uow.SaveChangesAsync();
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

            var message = await _uow.MessageRepository.FindAsync(id.Value, User.GetUserId());
            
            if (message == null)
            {
                return NotFound();
            }

            var vm = new MessageCreateEditViewModel()
            {
                ConversationSelectList = new SelectList(await _uow.ConversationRepository.AllAsync(User.GetUserId()),
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
                _uow.MessageRepository.Update(vm.Message);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            vm.ConversationSelectList = new SelectList(await _uow.ConversationRepository.AllAsync(User.GetUserId()),
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

            var entity = await _uow.MessageRepository.FindAsync(id.Value, User.GetUserId());
            
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
            await _uow.MessageRepository.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
