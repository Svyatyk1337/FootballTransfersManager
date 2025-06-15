using Microsoft.EntityFrameworkCore;
using FootballTransfers.Core.Entities;

public class ApplicationDbContext : DbContext
{
    public DbSet<Agent> Agents { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Transfer> Transfers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.Player)
            .WithMany(p => p.Transfers)
            .HasForeignKey(t => t.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.FromClub)
            .WithMany(c => c.TransfersFrom)
            .HasForeignKey(t => t.FromClubId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.ToClub)
            .WithMany(c => c.TransfersTo)
            .HasForeignKey(t => t.ToClubId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Player>()
            .HasOne(p => p.CurrentClub)
            .WithMany(c => c.CurrentPlayers)
            .HasForeignKey(p => p.CurrentClubId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Player>()
            .HasOne(p => p.Agent)
            .WithMany(a => a.Players)
            .HasForeignKey(p => p.AgentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
