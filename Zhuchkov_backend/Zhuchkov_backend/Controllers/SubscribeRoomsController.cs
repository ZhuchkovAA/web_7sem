using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zhuchkov_backend.Data;
using Zhuchkov_backend.Models;

namespace Zhuchkov_backend.Controllers
{
    public class SubscribeRoomsController : Controller
    {
        private readonly Zhuchkov_backendContext _context;

        public SubscribeRoomsController(Zhuchkov_backendContext context)
        {
            _context = context;
        }

        // GET: SubscribeRooms
        public async Task<IActionResult> Index()
        {
              return View(await _context.SubscribeRoom.ToListAsync());
        }

        // GET: SubscribeRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubscribeRoom == null)
            {
                return NotFound();
            }

            var subscribeRoom = await _context.SubscribeRoom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscribeRoom == null)
            {
                return NotFound();
            }

            return View(subscribeRoom);
        }

        // GET: SubscribeRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubscribeRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdTelegram,Date,IdRoom,IdTimeChunks")] SubscribeRoom subscribeRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subscribeRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subscribeRoom);
        }

        // GET: SubscribeRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubscribeRoom == null)
            {
                return NotFound();
            }

            var subscribeRoom = await _context.SubscribeRoom.FindAsync(id);
            if (subscribeRoom == null)
            {
                return NotFound();
            }
            return View(subscribeRoom);
        }

        // POST: SubscribeRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdTelegram,Date,IdRoom,IdTimeChunks")] SubscribeRoom subscribeRoom)
        {
            if (id != subscribeRoom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscribeRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscribeRoomExists(subscribeRoom.Id))
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
            return View(subscribeRoom);
        }

        // GET: SubscribeRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubscribeRoom == null)
            {
                return NotFound();
            }

            var subscribeRoom = await _context.SubscribeRoom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscribeRoom == null)
            {
                return NotFound();
            }

            return View(subscribeRoom);
        }

        // POST: SubscribeRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubscribeRoom == null)
            {
                return Problem("Entity set 'Zhuchkov_backendContext.SubscribeRoom'  is null.");
            }
            var subscribeRoom = await _context.SubscribeRoom.FindAsync(id);
            if (subscribeRoom != null)
            {
                _context.SubscribeRoom.Remove(subscribeRoom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubscribeRoomExists(int id)
        {
          return _context.SubscribeRoom.Any(e => e.Id == id);
        }
    }
}
