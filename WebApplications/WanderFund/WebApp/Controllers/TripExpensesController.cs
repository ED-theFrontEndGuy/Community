using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Controllers
{
    public class TripExpensesController : Controller
    {
        private readonly AppDbContext _context;

        public TripExpensesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TripExpenses
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TripExpenses.Include(t => t.Expense).Include(t => t.Trip);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TripExpenses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripExpense = await _context.TripExpenses
                .Include(t => t.Expense)
                .Include(t => t.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripExpense == null)
            {
                return NotFound();
            }

            return View(tripExpense);
        }

        // GET: TripExpenses/Create
        public IActionResult Create()
        {
            ViewData["ExpenseId"] = new SelectList(_context.Expenses, "Id", "CreatedBy");
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "CreatedBy");
            return View();
        }

        // POST: TripExpenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripId,ExpenseId,Id,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,SysNotes")] TripExpense tripExpense)
        {
            if (ModelState.IsValid)
            {
                tripExpense.Id = Guid.NewGuid();
                _context.Add(tripExpense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExpenseId"] = new SelectList(_context.Expenses, "Id", "CreatedBy", tripExpense.ExpenseId);
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "CreatedBy", tripExpense.TripId);
            return View(tripExpense);
        }

        // GET: TripExpenses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripExpense = await _context.TripExpenses.FindAsync(id);
            if (tripExpense == null)
            {
                return NotFound();
            }
            ViewData["ExpenseId"] = new SelectList(_context.Expenses, "Id", "CreatedBy", tripExpense.ExpenseId);
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "CreatedBy", tripExpense.TripId);
            return View(tripExpense);
        }

        // POST: TripExpenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TripId,ExpenseId,Id,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,SysNotes")] TripExpense tripExpense)
        {
            if (id != tripExpense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripExpense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExpenseExists(tripExpense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExpenseId"] = new SelectList(_context.Expenses, "Id", "CreatedBy", tripExpense.ExpenseId);
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "CreatedBy", tripExpense.TripId);
            return View(tripExpense);
        }

        // GET: TripExpenses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripExpense = await _context.TripExpenses
                .Include(t => t.Expense)
                .Include(t => t.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripExpense == null)
            {
                return NotFound();
            }

            return View(tripExpense);
        }

        // POST: TripExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tripExpense = await _context.TripExpenses.FindAsync(id);
            if (tripExpense != null)
            {
                _context.TripExpenses.Remove(tripExpense);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExpenseExists(Guid id)
        {
            return _context.TripExpenses.Any(e => e.Id == id);
        }
    }
}
