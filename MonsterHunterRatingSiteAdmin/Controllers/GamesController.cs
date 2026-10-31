using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonsterHunterRatingSiteAdmin.Models;
using MonsterHunterRatingSiteAdmin.Services;

public class GamesController : Controller
{
    private readonly MonsterHunterRatingSiteAdminContext _context;
    private readonly IAzureBlobService _blobService;

    public GamesController(MonsterHunterRatingSiteAdminContext context, IAzureBlobService blobService)
    {
        _context = context;
        _blobService = blobService;
    }

    // GET: GAMES
    public async Task<IActionResult> Index()
    {
        return View(await _context.Game.Include(g => g.Generation).Include(g => g.GameConsole).ToListAsync());
    }

    // GET: GAMES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Game
            .Include(g => g.Generation).Include(g => g.GameConsole)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (game == null)
        {
            return NotFound();
        }

        return View(game);
    }

    // GET: GAMES/Create
    public IActionResult Create()
    {
        ViewBag.GenerationId = new SelectList(_context.Generation, "Id", "Name");
        ViewBag.GameConsoleId = new SelectList(_context.GameConsole, "Id", "Name");
        return View();
    }

    // POST: GAMES/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,GameIcon,Description,GenerationId,GameConsoleId,NumMonster,ReleaseDate")] Game game, IFormFile? imageFile)
    {
        ModelState.Remove("GameIcon");
        if (imageFile == null || imageFile.Length == 0)
        {
            ModelState.AddModelError("GameIcon", "Please select a game icon");
        }
        else
        {
            string extension = Path.GetExtension(imageFile.FileName);
            string filename = $"{Guid.NewGuid()}{extension}";
            game.GameIcon = await _blobService.UploadFileAsync(imageFile, filename);
        }
        if (ModelState.IsValid)
        {
            _context.Add(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.GenerationId = new SelectList(_context.Generation, "Id", "Name", game.GenerationId);
        ViewBag.GameConsoleId = new SelectList(_context.GameConsole, "Id", "Name", game.GameConsoleId);
        return View(game);
    }

    // GET: GAMES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Game.FindAsync(id);
        if (game == null)
        {
            return NotFound();
        }
        ViewBag.GenerationId = new SelectList(_context.Generation, "Id", "Name", game.GenerationId);
        ViewBag.GameConsoleId = new SelectList(_context.GameConsole, "Id", "Name", game.GameConsoleId);
        return View(game);
    }

    // POST: GAMES/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,GameIcon,Description,GenerationId,GameConsoleId,NumMonster,ReleaseDate")] Game game)
    {
        if (id != game.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(game);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(game.Id))
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
        ViewBag.GenerationId = new SelectList(_context.Generation, "Id", "Name", game.GenerationId);
        ViewBag.GameConsoleId = new SelectList(_context.GameConsole, "Id", "Name", game.GameConsoleId);
        return View(game);
    }

    // GET: GAMES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Game
            .Include(g => g.Generation).Include(g => g.GameConsole)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (game == null)
        {
            return NotFound();
        }

        return View(game);
    }

    // POST: GAMES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var game = await _context.Game.FindAsync(id);
        if (game != null)
        {
            _context.Game.Remove(game);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GameExists(int? id)
    {
        return _context.Game.Any(e => e.Id == id);
    }
}