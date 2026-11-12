using Microsoft.EntityFrameworkCore;
using Sgirq.Domain.Entities;
namespace Sgirq.Infrastructure.Persistence;
public sealed class SgirqDbContext(DbContextOptions<SgirqDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Personal> Personal => Set<Personal>();
    public DbSet<Reactivo> Reactivos => Set<Reactivo>();
    public DbSet<Salida> Salidas => Set<Salida>();
    public DbSet<Requiere> Requiere => Set<Requiere>();
    public DbSet<Reporte> Reportes => Set<Reporte>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.ApplyConfigurationsFromAssembly(typeof(SgirqDbContext).Assembly);
    }
}
