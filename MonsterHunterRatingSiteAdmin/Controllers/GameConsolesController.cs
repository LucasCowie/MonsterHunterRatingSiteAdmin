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
    public class GameConsolesController : Controller
    {
        private readonly MonsterHunterRatingSiteAdminContext _context;

        public GameConsolesController(MonsterHunterRatingSiteAdminContext context)
        {
            _context = context;
        }

        // GET: GameConsoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameConsole.ToListAsync());
        }

        // GET: GameConsoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameConsole = await _context.GameConsole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameConsole == null)
            {
                return NotFound();
            }

            return View(gameConsole);
        }

        // GET: GameConsoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameConsoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GameConsole gameConsole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameConsole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameConsole);
        }

        // GET: GameConsoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameConsole = await _context.GameConsole.FindAsync(id);
            if (gameConsole == null)
            {
                return NotFound();
            }
            return View(gameConsole);
        }

        // POST: GameConsoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GameConsole gameConsole)
        {
            if (id != gameConsole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameConsole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameConsoleExists(gameConsole.Id))
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
            return View(gameConsole);
        }

        // GET: GameConsoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameConsole = await _context.GameConsole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameConsole == null)
            {
                return NotFound();
            }

            return View(gameConsole);
        }

        // POST: GameConsoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameConsole = await _context.GameConsole.FindAsync(id);
            if (gameConsole != null)
            {
                _context.GameConsole.Remove(gameConsole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameConsoleExists(int id)
        {
            return _context.GameConsole.Any(e => e.Id == id);
        }
    }
}
