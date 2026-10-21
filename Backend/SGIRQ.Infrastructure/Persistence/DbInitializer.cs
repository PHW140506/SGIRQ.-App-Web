using Microsoft.EntityFrameworkCore;
using SGIRQ.Application.Interfaces;
using SGIRQ.Domain.Entities;

namespace SGIRQ.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task InicializarAsync(SgirqDbContext context, IPasswordHasher hasher)
    {
        await context.Database.EnsureCreatedAsync();

        // Si la tabla no tiene los usuarios de prueba, los agregamos
        if (!await context.Usuarios.AnyAsync(u => u.Username == "mgarcia"))
        {
            var hashComun = hasher.HashPassword("Admin123*");

            var usuariosPrueba = new List<Usuario>
            {
                // Escenario 1: Administrador con acceso total
                new("Jorge Rodríguez", "jrodriguez", "jrodriguez@labquim.com", hashComun, "Administrador"),
                // Escenario 2: Auxiliar bloqueado en Usuarios
                new("María García", "mgarcia", "mgarcia@labquim.com", hashComun, "Auxiliar"),
                // Escenario 3: Solicitante bloqueado en inventario
                new("Ana Vargas", "avargas", "avargas@labquim.com", hashComun, "Solicitante")
            };

            foreach (var u in usuariosPrueba)
            {
                if (!await context.Usuarios.AnyAsync(x => x.Username == u.Username))
                {
                    await context.Usuarios.AddAsync(u);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}