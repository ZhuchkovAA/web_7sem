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
    public class TimeChunksController : Controller
    {
        private readonly Zhuchkov_backendContext _context;

        public TimeChunksController(Zhuchkov_backendContext context)
        {
            _context = context;
        }

        // GET: TimeChunks
        public async Task<IActionResult> Index()
        {
              return View(await _context.TimeChunk.ToListAsync());
        }

        // GET: TimeChunks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TimeChunk == null)
            {
                return NotFound();
            }

            var timeChunk = await _context.TimeChunk
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeChunk == null)
            {
                return NotFound();
            }

            return View(timeChunk);
        }

        // GET: TimeChunks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TimeChunks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Time")] TimeChunk timeChunk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeChunk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timeChunk);
        }

        // GET: TimeChunks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TimeChunk == null)
            {
                return NotFound();
            }

            var timeChunk = await _context.TimeChunk.FindAsync(id);
            if (timeChunk == null)
            {
                return NotFound();
            }
            return View(timeChunk);
        }

        // POST: TimeChunks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Time")] TimeChunk timeChunk)
        {
            if (id != timeChunk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeChunk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeChunkExists(timeChunk.Id))
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
            return View(timeChunk);
        }

        // GET: TimeChunks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TimeChunk == null)
            {
                return NotFound();
            }

            var timeChunk = await _context.TimeChunk
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeChunk == null)
            {
                return NotFound();
            }

            return View(timeChunk);
        }

        // POST: TimeChunks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TimeChunk == null)
            {
                return Problem("Entity set 'Zhuchkov_backendContext.TimeChunk'  is null.");
            }
            var timeChunk = await _context.TimeChunk.FindAsync(id);
            if (timeChunk != null)
            {
                _context.TimeChunk.Remove(timeChunk);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeChunkExists(int id)
        {
          return _context.TimeChunk.Any(e => e.Id == id);
        }
    }
}
