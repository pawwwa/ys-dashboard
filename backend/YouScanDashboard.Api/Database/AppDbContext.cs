using Microsoft.EntityFrameworkCore;
using YouScanDashboard.Api.Database.Entities;

namespace YouScanDashboard.Api.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Widget> Widgets => Set<Widget>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Widget>(entity =>
        {
            entity.HasKey(w => w.Id);

            entity.Property(w => w.Type)
                .HasConversion<string>()
                .HasMaxLength(32)
                .IsRequired();

            entity.Property(w => w.Text)
                .HasMaxLength(5000);

            entity.HasIndex(w => w.Position);

            entity.OwnsMany(w => w.Points, points => points.ToJson());
        });
    }
}
