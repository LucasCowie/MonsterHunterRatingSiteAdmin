using System.ComponentModel.DataAnnotations;

namespace MonsterHunterRatingSiteAdmin.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
