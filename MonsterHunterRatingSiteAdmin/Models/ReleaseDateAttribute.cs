using System.ComponentModel.DataAnnotations;

namespace MonsterHunterRatingSiteAdmin.Models
{
    // Culture-independent release date check (Range(typeof(DateTime), ...) parses its bounds with the current culture)
    public class ReleaseDateAttribute : ValidationAttribute
    {
        public static readonly DateTime EarliestDate = new(2004, 3, 11); // first Monster Hunter release
        public static readonly DateTime LatestDate = new(2100, 1, 1);

        public ReleaseDateAttribute()
            : base("Release date must be between 03/11/2004 (the first Monster Hunter release) and 01/01/2100.")
        {
        }

        public override bool IsValid(object? value)
        {
            return value is DateTime date && date.Date >= EarliestDate && date.Date <= LatestDate;
        }
    }
}
