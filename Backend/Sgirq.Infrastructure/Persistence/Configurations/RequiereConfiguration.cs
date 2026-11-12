using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgirq.Domain.Entities;
namespace Sgirq.Infrastructure.Persistence.Configurations;
public sealed class RequiereConfiguration : IEntityTypeConfiguration<Requiere>
{
    public void Configure(EntityTypeBuilder<Requiere> b)
    {
        b.ToTable("Requiere", t => t.HasCheckConstraint("CK_Requiere_Cantidad", "cantidadSolicitada > 0"));
        b.HasKey(x => new { x.IdSalida, x.IdReactivo });
        b.Property(x => x.IdSalida).HasColumnName("idSalida");
        b.Property(x => x.IdReactivo).HasColumnName("idReactivo");
        b.Property(x => x.CantidadSolicitada).HasColumnName("cantidadSolicitada").HasPrecision(10, 2);
        b.HasOne<Reactivo>().WithMany().HasForeignKey(x => x.IdReactivo).OnDelete(DeleteBehavior.Restrict);
        b.HasOne<Salida>().WithMany(x => x.Requiere).HasForeignKey(x => x.IdSalida).OnDelete(DeleteBehavior.Cascade);
    }
}
