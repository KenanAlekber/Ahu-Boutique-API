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

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductImage> ProductImages { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Slider> Sliders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<ProductImage>().HasQueryFilter(pi => !pi.IsDeleted);
        modelBuilder.Entity<Brand>().HasQueryFilter(b => !b.IsDeleted);
        modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<Slider>().HasQueryFilter(s => !s.IsDeleted);
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