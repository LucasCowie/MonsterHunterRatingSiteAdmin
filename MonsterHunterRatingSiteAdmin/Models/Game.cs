using System.ComponentModel.DataAnnotations;

namespace MonsterHunterRatingSiteAdmin.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the game's title.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Game Icon")]
        [Required(ErrorMessage = "Please enter an icon URL.")]
        [Url(ErrorMessage = "Game icon must be a valid URL (e.g. https://example.com/icon.png).")]
        [StringLength(500, ErrorMessage = "Icon URL can't be longer than 500 characters.")]
        public string GameIcon { get; set; } = string.Empty; //Url or link to the img for the game

        [Required(ErrorMessage = "Please enter a description.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 1000 characters.")]
        public string Description { get; set; } = string.Empty; //Little description of the game

        [Required(ErrorMessage = "Please select a generation.")]
        [AllowedValues("1st Generation", "2nd Generation", "3rd Generation", "4th Generation", "5th Generation", "6th Generation",
            ErrorMessage = "Please select a generation from the list.")]
        public string Generation { get; set; } = string.Empty; //The Devlmepot period the game was made in

        [Required(ErrorMessage = "Please select a console.")]
        [AllowedValues("PlayStation 2", "PlayStation Portable", "Wii", "Nintendo 3DS", "Wii U", "PlayStation 4", "Xbox One",
            "Nintendo Switch", "Nintendo Switch 2", "PlayStation 5", "Xbox Series X|S", "PC",
            ErrorMessage = "Please select a console from the list.")]
        public string Console { get; set; } = string.Empty; //The console the game was insinally made for

        [Display(Name = "Number of Monsters")]
        [Range(1, 500, ErrorMessage = "Number of monsters must be between 1 and 500.")]
        public int NumMonster { get; set; } //The Num of monster present at the end of the game (Including tilte updates)

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [ReleaseDate]
        public DateTime ReleaseDate { get; set; }
    }
}
