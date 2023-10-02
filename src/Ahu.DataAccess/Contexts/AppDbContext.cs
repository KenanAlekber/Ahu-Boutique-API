using Ahu.Core.Entities;
using Ahu.Core.Entities.Common;
using Ahu.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Ahu.DataAccess.Contexts;

public class AppDbContext : DbContext   
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Clother> Clothers { get; set; } = null!;
    public DbSet<ClotherImage> ClothersImages { get; set; } = null!;
    public DbSet<Shoe> Shoes { get; set; } = null!;
    public DbSet<ShoeImage> ShoeImages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClotherConfiguration).Assembly);
        modelBuilder.Entity<Clother>().HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<ClotherImage>().HasQueryFilter(ci => !ci.IsDeleted);
        modelBuilder.Entity<Shoe>().HasQueryFilter(s => !s.IsDeleted);
        modelBuilder.Entity<ShoeImage>().HasQueryFilter(si => !si.IsDeleted);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseSectionEntity>();
        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedTime = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "Admin";
                    entry.Entity.UpdatedTime = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = "Admin";
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedTime = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = "Admin";
                    break;
                default:
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}