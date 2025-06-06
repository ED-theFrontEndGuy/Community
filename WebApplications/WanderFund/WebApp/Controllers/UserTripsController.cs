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
    public class UserTripsController : Controller
    {
        private readonly AppDbContext _context;

        public UserTripsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserTrips
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserTrips.Include(u => u.Trip).Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserTrips/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTrip = await _context.UserTrips
                .Include(u => u.Trip)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTrip == null)
            {
                return NotFound();
            }

            return View(userTrip);
        }

        // GET: UserTrips/Create
        public IActionResult Create()
        {
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "CreatedBy");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: UserTrips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IsUserTripAdmin,TripId,UserId,Id,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,SysNotes")] UserTrip userTrip)
        {
            if (ModelState.IsValid)
            {
                userTrip.Id = Guid.NewGuid();
                _context.Add(userTrip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "CreatedBy", userTrip.TripId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userTrip.UserId);
            return View(userTrip);
        }

        // GET: UserTrips/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTrip = await _context.UserTrips.FindAsync(id);
            if (userTrip == null)
            {
                return NotFound();
            }
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "CreatedBy", userTrip.TripId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userTrip.UserId);
            return View(userTrip);
        }

        // POST: UserTrips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IsUserTripAdmin,TripId,UserId,Id,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,SysNotes")] UserTrip userTrip)
        {
            if (id != userTrip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTrip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTripExists(userTrip.Id))
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
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "CreatedBy", userTrip.TripId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userTrip.UserId);
            return View(userTrip);
        }

        // GET: UserTrips/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTrip = await _context.UserTrips
                .Include(u => u.Trip)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTrip == null)
            {
                return NotFound();
            }

            return View(userTrip);
        }

        // POST: UserTrips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userTrip = await _context.UserTrips.FindAsync(id);
            if (userTrip != null)
            {
                _context.UserTrips.Remove(userTrip);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTripExists(Guid id)
        {
            return _context.UserTrips.Any(e => e.Id == id);
        }
    }
}
