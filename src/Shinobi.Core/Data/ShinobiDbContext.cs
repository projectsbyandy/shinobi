using Microsoft.EntityFrameworkCore;
using Shinobi.Core.Models;

namespace Shinobi.Core.Data;

public class ShinobiDbContext : DbContext
{ 
    public ShinobiDbContext(DbContextOptions<ShinobiDbContext> options)
        : base(options)
    {
    }
    
    public ShinobiDbContext()
    {
    }

    public virtual DbSet<Ninja> Ninja { get; set; }

    public virtual DbSet<Skill> Skill { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ninja>(entity =>
        {
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnName("ID");
        });
        
        modelBuilder.Entity<Skill>(entity =>
        {
            entity.Property(e => e.Details)
                .HasMaxLength(255)
                .IsUnicode(false);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}
