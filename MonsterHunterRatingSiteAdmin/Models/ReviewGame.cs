using System.ComponentModel.DataAnnotations;

namespace MonsterHunterRatingSiteAdmin.Models
{
    public class ReviewGame
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 1000 characters.")]
        public string Description { get; set; } = string.Empty; //Little description of the game

        public int Rating {  get; set; }
        public bool isPublished { get; set; } = false;
        public string createdBy { get; set; } = string.Empty;
        public DateTime createedDate { get; set; } = DateTime.Now;
    }
}
