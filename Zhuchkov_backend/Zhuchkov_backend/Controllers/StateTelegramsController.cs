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
    public class StateTelegramsController : Controller
    {
        private readonly Zhuchkov_backendContext _context;

        public StateTelegramsController(Zhuchkov_backendContext context)
        {
            _context = context;
        }

        // GET: StateTelegrams
        public async Task<IActionResult> Index()
        {
              return View(await _context.StateTelegram.ToListAsync());
        }

        // GET: StateTelegrams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StateTelegram == null)
            {
                return NotFound();
            }

            var stateTelegram = await _context.StateTelegram
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stateTelegram == null)
            {
                return NotFound();
            }

            return View(stateTelegram);
        }

        // GET: StateTelegrams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StateTelegrams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] StateTelegram stateTelegram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stateTelegram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stateTelegram);
        }

        // GET: StateTelegrams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StateTelegram == null)
            {
                return NotFound();
            }

            var stateTelegram = await _context.StateTelegram.FindAsync(id);
            if (stateTelegram == null)
            {
                return NotFound();
            }
            return View(stateTelegram);
        }

        // POST: StateTelegrams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] StateTelegram stateTelegram)
        {
            if (id != stateTelegram.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stateTelegram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateTelegramExists(stateTelegram.Id))
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
            return View(stateTelegram);
        }

        // GET: StateTelegrams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StateTelegram == null)
            {
                return NotFound();
            }

            var stateTelegram = await _context.StateTelegram
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stateTelegram == null)
            {
                return NotFound();
            }

            return View(stateTelegram);
        }

        // POST: StateTelegrams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StateTelegram == null)
            {
                return Problem("Entity set 'Zhuchkov_backendContext.StateTelegram'  is null.");
            }
            var stateTelegram = await _context.StateTelegram.FindAsync(id);
            if (stateTelegram != null)
            {
                _context.StateTelegram.Remove(stateTelegram);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateTelegramExists(int id)
        {
          return _context.StateTelegram.Any(e => e.Id == id);
        }
    }
}
