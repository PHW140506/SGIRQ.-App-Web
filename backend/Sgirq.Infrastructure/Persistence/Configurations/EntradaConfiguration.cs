using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgirq.Domain.Entities;
namespace Sgirq.Infrastructure.Persistence.Configurations;
public sealed class EntradaConfiguration : IEntityTypeConfiguration<Entrada>
{
    public void Configure(EntityTypeBuilder<Entrada> b)
    {
        b.ToTable("Entrada", t => t.HasCheckConstraint("CK_Entrada_Cantidad", "cantidad > 0"));
        b.HasKey(x => x.IdEntrada);
        b.Property(x => x.IdEntrada).HasColumnName("idEntrada");
        b.Property(x => x.IdReactivo).HasColumnName("idReactivo");
        b.Property(x => x.Cantidad).HasColumnName("cantidad").HasPrecision(10, 2);
        b.Property(x => x.FechaEntrada).HasColumnName("fechaEntrada").HasColumnType("datetime");
        b.HasIndex(x => x.FechaEntrada);
        b.HasOne<Reactivo>().WithMany().HasForeignKey(x => x.IdReactivo).OnDelete(DeleteBehavior.Restrict);
    }
}
