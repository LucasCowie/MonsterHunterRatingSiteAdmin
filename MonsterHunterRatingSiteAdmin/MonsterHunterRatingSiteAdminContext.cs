using Microsoft.EntityFrameworkCore;

public class MonsterHunterRatingSiteAdminContext(DbContextOptions<MonsterHunterRatingSiteAdminContext> options) : DbContext(options)
{
    public DbSet<MonsterHunterRatingSiteAdmin.Models.Game> Game { get; set; } = default!;
}
