using Microsoft.EntityFrameworkCore;
using SGIRQ.Domain.Entities;

namespace SGIRQ.Infrastructure.Persistence;

public class SgirqDbContext : DbContext
{
    public SgirqDbContext(DbContextOptions<SgirqDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Username).IsUnique();
            entity.HasIndex(u => u.Correo).IsUnique();
            entity.Property(u => u.Username).HasMaxLength(50).IsRequired();
            entity.Property(u => u.Correo).HasMaxLength(150).IsRequired();
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.Rol).HasMaxLength(30).IsRequired();
        });
    }
}