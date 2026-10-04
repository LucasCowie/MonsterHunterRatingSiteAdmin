using Microsoft.AspNetCore.Mvc.Rendering;

namespace MonsterHunterRatingSiteAdmin.Models
{
    // Fixed lists of values allowed for Game.Generation and Game.Console
    public static class GameOptions
    {
        public static readonly string[] Generations =
        {
            "1st Generation",
            "2nd Generation",
            "3rd Generation",
            "4th Generation",
            "5th Generation",
            "6th Generation"
        };

        public static readonly string[] Consoles =
        {
            "PlayStation 2",
            "PlayStation Portable",
            "Wii",
            "Nintendo 3DS",
            "Wii U",
            "PlayStation 4",
            "Xbox One",
            "Nintendo Switch",
            "Nintendo Switch 2",
            "PlayStation 5",
            "Xbox Series X|S",
            "PC"
        };

        public static IEnumerable<SelectListItem> GenerationItems =>
            Generations.Select(g => new SelectListItem(g, g));

        public static IEnumerable<SelectListItem> ConsoleItems =>
            Consoles.Select(c => new SelectListItem(c, c));
    }
}
