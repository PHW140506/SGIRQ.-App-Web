using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgirq.Domain.Entities;
namespace Sgirq.Infrastructure.Persistence.Configurations;
public sealed class ReporteConfiguration : IEntityTypeConfiguration<Reporte>
{
    public void Configure(EntityTypeBuilder<Reporte> b)
    {
        b.ToTable("Reporte"); b.HasKey(x => x.IdReporte);
        b.Property(x => x.IdReporte).HasColumnName("idReporte");
        b.Property(x => x.FechaInicio).HasColumnName("fechaInicio").HasColumnType("date");
        b.Property(x => x.FechaFinal).HasColumnName("fechaFinal").HasColumnType("date");
        b.Property(x => x.TipoReporte).HasColumnName("tipoReporte").HasMaxLength(30).IsRequired();
        b.Property(x => x.IdPersonal).HasColumnName("idPersonal");
        b.HasOne<Personal>().WithMany().HasForeignKey(x => x.IdPersonal).OnDelete(DeleteBehavior.Restrict);
    }
}
