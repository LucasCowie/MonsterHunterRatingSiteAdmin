using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace MonsterHunterRatingSiteAdmin.Models
{
    // One review per critic per game
    [Index(nameof(GameId), nameof(CreatedBy), IsUnique = true)]
    public class ReviewGame
    {
        public int Id { get; set; }

        [Display(Name = "Game")]
        [Required(ErrorMessage = "Please select a game.")]
        public int GameId { get; set; }
        public Game? Game { get; set; }

        [Display(Name = "Review")]
        [Required(ErrorMessage = "Please enter your review.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Review must be between 10 and 1000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int Rating { get; set; }

        [Display(Name = "Published")]
        public bool IsPublished { get; set; } = false;

        // Auth0 user id of the critic; set by the controller, never from the form
        [ValidateNever]
        [StringLength(450)]
        public string CreatedBy { get; set; } = string.Empty;

        [Display(Name = "Created")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
