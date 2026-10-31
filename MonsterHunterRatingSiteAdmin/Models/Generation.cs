using System.ComponentModel.DataAnnotations;

namespace MonsterHunterRatingSiteAdmin.Models
{
    public class Generation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a generation name.")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}