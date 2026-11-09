using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonsterHunterRatingSiteAdmin.Models;

namespace MonsterHunterRatingSiteAdmin.Controllers
{
    // Critics only ever see and change their own reviews; published reviews are read-only
    [Authorize(Roles = "critic")]
    public class ReviewsController : Controller
    {
        private readonly MonsterHunterRatingSiteAdminContext _context;

        public ReviewsController(MonsterHunterRatingSiteAdminContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserId();
            return View(await _context.ReviewGame
                .Include(r => r.Game)
                .Where(r => r.CreatedBy == userId)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var review = await FindOwnReviewAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create?gameId=5
        public async Task<IActionResult> Create(int? gameId)
        {
            await LoadUnreviewedGamesAsync(gameId);
            return View(new ReviewGame { GameId = gameId ?? 0 });
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,Description,Rating")] ReviewGame review)
        {
            review.CreatedBy = User.GetUserId();
            review.CreatedDate = DateTime.Now;
            review.IsPublished = false;

            if (await _context.ReviewGame.AnyAsync(r => r.GameId == review.GameId && r.CreatedBy == review.CreatedBy))
            {
                ModelState.AddModelError("GameId", "You have already reviewed this game.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await LoadUnreviewedGamesAsync(review.GameId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var review = await FindOwnReviewAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            if (review.IsPublished)
            {
                return BadRequest("Published reviews can't be changed.");
            }

            return View(review);
        }

        // POST: Reviews/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            var review = await FindOwnReviewAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            if (review.IsPublished)
            {
                return BadRequest("Published reviews can't be changed.");
            }

            // Only the text and rating are editable; game, owner and status come from the database
            if (await TryUpdateModelAsync(review, "", r => r.Description, r => r.Rating) && ModelState.IsValid)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // POST: Reviews/Publish/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Publish(int? id)
        {
            var review = await FindOwnReviewAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            review.IsPublished = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var review = await FindOwnReviewAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            if (review.IsPublished)
            {
                return BadRequest("Published reviews can't be deleted.");
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var review = await FindOwnReviewAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            if (review.IsPublished)
            {
                return BadRequest("Published reviews can't be deleted.");
            }

            _context.ReviewGame.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private Task<ReviewGame?> FindOwnReviewAsync(int? id)
        {
            var userId = User.GetUserId();
            return _context.ReviewGame
                .Include(r => r.Game)
                .FirstOrDefaultAsync(r => r.Id == id && r.CreatedBy == userId);
        }

        private async Task LoadUnreviewedGamesAsync(int? selectedGameId)
        {
            var userId = User.GetUserId();
            var games = await _context.Game
                .Where(g => !_context.ReviewGame.Any(r => r.GameId == g.Id && r.CreatedBy == userId))
                .OrderBy(g => g.Title)
                .ToListAsync();
            ViewBag.GameId = new SelectList(games, "Id", "Title", selectedGameId);
        }
    }
}
