using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgirq.Domain.Entities;
namespace Sgirq.Infrastructure.Persistence.Configurations;
public sealed class PersonalConfiguration : IEntityTypeConfiguration<Personal>
{
    public void Configure(EntityTypeBuilder<Personal> b)
    {
        b.ToTable("Personal"); b.HasKey(x => x.IdPersonal);
        b.Property(x => x.IdPersonal).HasColumnName("idPersonal");
        b.Property(x => x.NombrePersonal).HasColumnName("nombrePersonal").HasMaxLength(30).IsRequired();
        b.Property(x => x.ApellidosPersonal).HasColumnName("apellidosPersonal").HasMaxLength(45).IsRequired();
        b.Property(x => x.UsuarioPersonalLogin).HasColumnName("usuarioPersonalLogin").HasMaxLength(25).IsRequired();
        b.Property(x => x.Contrasenia).HasColumnName("contrasenia").HasMaxLength(255).IsRequired();
        b.Property(x => x.TipoPersonal).HasColumnName("tipoPersonal").HasColumnType("enum('Administrador','Auxiliar','Solicitante')");
        b.HasIndex(x => x.UsuarioPersonalLogin).IsUnique();
    }
}
