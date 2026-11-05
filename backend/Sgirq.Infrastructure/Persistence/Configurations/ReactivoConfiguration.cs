using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgirq.Domain.Entities;
namespace Sgirq.Infrastructure.Persistence.Configurations;
public sealed class ReactivoConfiguration : IEntityTypeConfiguration<Reactivo>
{
    public void Configure(EntityTypeBuilder<Reactivo> b)
    {
        b.ToTable("Reactivos", t => t.HasCheckConstraint("CK_Reactivos_Cantidad", "cantidadReactivo >= 0 AND stockMinimo >= 0"));
        b.HasKey(x => x.IdReactivo);
        b.Property(x => x.IdReactivo).HasColumnName("idReactivo");
        b.Property(x => x.NombreComercial).HasColumnName("nombreComercial").HasMaxLength(50).IsRequired();
        b.Property(x => x.NombreQuimico).HasColumnName("nombreQuimico").HasMaxLength(50).IsRequired();
        b.Property(x => x.NumeroCAS).HasColumnName("numeroCAS").HasMaxLength(30).IsRequired();
        b.Property(x => x.EstadoFisico).HasColumnName("estadoFisico").HasColumnType("enum('Líquido','Sólido')");
        b.Property(x => x.CantidadReactivo).HasColumnName("cantidadReactivo").HasPrecision(10, 2);
        b.Property(x => x.UnidadMedida).HasColumnName("unidadMedida")
            .HasColumnType("enum('Mililitro','Gramo','Litro','Kilogramo')");
        b.Property(x => x.StockMinimo).HasColumnName("stockMinimo").HasPrecision(10, 2);
        b.Property(x => x.ColorAnaquel).HasColumnName("colorAnaquel")
            .HasColumnType("enum('Verde','Rojo','Azul','Amarillo','Blanco','Naranja')");
        b.Property(x => x.IdPersonal).HasColumnName("idPersonal");
        b.HasIndex(x => x.NumeroCAS).IsUnique();
        b.HasOne<Personal>().WithMany().HasForeignKey(x => x.IdPersonal).OnDelete(DeleteBehavior.Restrict);
    }
}
