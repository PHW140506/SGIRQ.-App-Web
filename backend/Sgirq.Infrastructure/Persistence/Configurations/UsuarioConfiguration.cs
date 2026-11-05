using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgirq.Domain.Entities;
namespace Sgirq.Infrastructure.Persistence.Configurations;
public sealed class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> b)
    {
        b.ToTable("Usuario");
        b.HasKey(x => x.IdUsuario);
        b.Property(x => x.IdUsuario).HasColumnName("idUsuario").ValueGeneratedOnAdd();
    }
}
