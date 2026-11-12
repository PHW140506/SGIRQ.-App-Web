using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgirq.Domain.Entities;
namespace Sgirq.Infrastructure.Persistence.Configurations;
public sealed class SalidaConfiguration : IEntityTypeConfiguration<Salida>
{
    public void Configure(EntityTypeBuilder<Salida> b)
    {
        b.ToTable("Salida"); b.HasKey(x => x.IdSalida);
        b.Property(x => x.IdSalida).HasColumnName("idSalida").ValueGeneratedNever();
        b.Property(x => x.FechaSalida).HasColumnName("fechaSalida").HasColumnType("datetime");
        b.Property(x => x.MotivoSalida).HasColumnName("motivoSalida").HasMaxLength(255).IsRequired();
        b.Property(x => x.IdUsuario).HasColumnName("idUsuario");
        b.Property(x => x.IdPersonal).HasColumnName("idPersonal");
        b.HasOne<Usuario>().WithMany().HasForeignKey(x => x.IdUsuario).OnDelete(DeleteBehavior.Restrict);
        b.HasOne<Personal>().WithMany().HasForeignKey(x => x.IdPersonal).OnDelete(DeleteBehavior.Restrict);
        b.HasIndex(x => x.FechaSalida);
    }
}
