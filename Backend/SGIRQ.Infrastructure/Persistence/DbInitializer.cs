using Microsoft.EntityFrameworkCore;
using SGIRQ.Application.Interfaces;
using SGIRQ.Domain.Entities;

namespace SGIRQ.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task InicializarAsync(SgirqDbContext context, IPasswordHasher hasher)
    {
        await context.Database.EnsureCreatedAsync();

        if (!await context.Usuarios.AnyAsync())
        {
            // Usuario Administrador inicial de prueba (conforme al PDF: jrodriguez)
            var admin = new Usuario(
                nombreCompleto: "Jorge Rodríguez",
                username: "jrodriguez",
                correo: "jrodriguez@labquim.com",
                passwordHash: hasher.HashPassword("Admin123*"),
                rol: "Administrador"
            );

            await context.Usuarios.AddAsync(admin);
            await context.SaveChangesAsync();
        }
    }
}