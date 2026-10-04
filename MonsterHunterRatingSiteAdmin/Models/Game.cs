
namespace MonsterHunterRatingSiteAdmin.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string GameIcon { get; set; } = string.Empty; //Url or link to the img for the game
        public string Description { get; set; } = string.Empty; //Little description of the game
        public string Generation { get; set; } = string.Empty; //The Devlmepot period the game was made in
        public string Console { get; set; } = string.Empty; //The console the game was insinally made for
        public int NumMonster { get; set; } //The Num of monster present at the end of the game (Including tilte updates)
        public DateTime ReleaseDate { get; set; }
    }
}