using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonsterHunterRatingSiteAdmin.Models;

namespace MonsterHunterRatingSiteAdmin.Controllers
{
    public class GenerationsController : Controller
    {
        private readonly MonsterHunterRatingSiteAdminContext _context;

        public GenerationsController(MonsterHunterRatingSiteAdminContext context)
        {
            _context = context;
        }

        // GET: Generations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Generation.ToListAsync());
        }

        // GET: Generations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generation = await _context.Generation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generation == null)
            {
                return NotFound();
            }

            return View(generation);
        }

        // GET: Generations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Generation generation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(generation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(generation);
        }

        // GET: Generations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generation = await _context.Generation.FindAsync(id);
            if (generation == null)
            {
                return NotFound();
            }
            return View(generation);
        }

        // POST: Generations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Generation generation)
        {
            if (id != generation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenerationExists(generation.Id))
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
            return View(generation);
        }

        // GET: Generations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generation = await _context.Generation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generation == null)
            {
                return NotFound();
            }

            return View(generation);
        }

        // POST: Generations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var generation = await _context.Generation.FindAsync(id);
            if (generation != null)
            {
                _context.Generation.Remove(generation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenerationExists(int id)
        {
            return _context.Generation.Any(e => e.Id == id);
        }
    }
}
