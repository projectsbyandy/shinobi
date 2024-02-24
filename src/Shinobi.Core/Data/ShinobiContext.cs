using Microsoft.EntityFrameworkCore;
using Shinobi.Core.Models;
using Shinobi.Core.Models.Config;

namespace Shinobi.Core.Data;

public class ShinobiContext : DbContext
{
    private readonly SqlConnectionDetails _sqlConnectionDetails;
    
    public ShinobiContext(SqlConnectionDetails sqlConnectionDetails)
    {
        _sqlConnectionDetails = sqlConnectionDetails;
    }

    public ShinobiContext(DbContextOptions<ShinobiContext> options, SqlConnectionDetails sqlConnectionDetails)
        : base(options)
    {
        _sqlConnectionDetails = sqlConnectionDetails;
    }

    public virtual DbSet<Ninja> Ninja { get; set; }

    public virtual DbSet<Skill> Skill { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer($"Server={_sqlConnectionDetails.Server}; Initial Catalog={_sqlConnectionDetails.Catalog}; " +
                                       $"user={_sqlConnectionDetails.UserName};Password={_sqlConnectionDetails.Password};TrustServerCertificate=True;");

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
