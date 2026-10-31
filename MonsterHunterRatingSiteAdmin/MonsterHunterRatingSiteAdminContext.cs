using Microsoft.EntityFrameworkCore;
using MonsterHunterRatingSiteAdmin.Models;

public class MonsterHunterRatingSiteAdminContext(DbContextOptions<MonsterHunterRatingSiteAdminContext> options) : DbContext(options)
{
    public DbSet<MonsterHunterRatingSiteAdmin.Models.Game> Game { get; set; } = default!;
    public DbSet<Generation> Generation { get; set; } = default!;
    public DbSet<GameConsole> GameConsole { get; set; } = default!;
}
