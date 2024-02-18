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

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<Skills> Skills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer($"Server={_sqlConnectionDetails.Server}; Initial Catalog={_sqlConnectionDetails.Catalog}; " +
                                       $"user={_sqlConnectionDetails.UserName};Password={_sqlConnectionDetails.Password};TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
        });
        
        modelBuilder.Entity<Skills>(entity =>
        {
            entity.HasNoKey();
        
            entity.Property(e => e.Details)
                .HasMaxLength(255)
                .IsUnicode(false);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}
