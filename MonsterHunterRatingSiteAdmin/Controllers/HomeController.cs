using Microsoft.AspNetCore.Mvc;
using MonsterHunterRatingSiteAdmin.Models;
using System.Diagnostics;

namespace MonsterHunterRatingSiteAdmin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult GetAllGames(){
            //Create a sample game
            List<Game> games = new List<Game>();

            games.Add(new Game(){
                Id = 1,
                Title = "Monster Hunter World",
                Description = "First game of the Fifth generation",
                Generation = "Fifth Generation",
                Console = "PlayStation / Xbox / PC",
                NumMonster = 32,
                ReleaseDate = new DateTime(2018, 1, 26),

                GameIcon = "GameIconURL",
            });

            games.Add(new Game(){
                Id = 2,
                Title = "Monster Hunter World: Iceborne",
                Description = "Expansion for Monster Hunter World",
                Generation = "Fifth Generation",
                Console = "PlayStation / Xbox / PC",
                NumMonster = 16,
                ReleaseDate = new DateTime(2019, 9, 6),

                GameIcon = "GameIconURL",
            });

            games.Add(new Game(){
                Id = 3,
                Title = "Monster Hunter Rise",
                Description = "Fifth generation title for the Switch",
                Generation = "Fifth Generation",
                Console = "Switch",
                NumMonster = 20,
                ReleaseDate = new DateTime(2021, 3, 26),

                GameIcon = "GameIconURL",
            });

            return View(games);
        }
            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
