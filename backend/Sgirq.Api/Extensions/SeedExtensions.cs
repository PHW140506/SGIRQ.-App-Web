using Microsoft.EntityFrameworkCore;
using Sgirq.Application.Abstractions;
using Sgirq.Domain.Entities;
using Sgirq.Infrastructure.Persistence;
namespace Sgirq.Api.Extensions;
public static class SeedExtensions
{
    public static async Task SeedDevelopmentAsync(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<SgirqDbContext>();
        // Las migraciones se ejecutan manualmente: dotnet ef database update.
        var password = app.Configuration["Seed:AdminPassword"];
        if (string.IsNullOrWhiteSpace(password)) return;
        if (!await db.Personal.AnyAsync())
        {
            var hasher = scope.ServiceProvider.GetRequiredService<IPasswords>();
            db.Personal.Add(new Personal { NombrePersonal = "Administrador",
                ApellidosPersonal = "SGIRQ", UsuarioPersonalLogin = "admin",
                Contrasenia = hasher.Hash(password), TipoPersonal = "Administrador" });
        }
        if (!await db.Usuarios.AnyAsync()) db.Usuarios.Add(new Usuario());
        await db.SaveChangesAsync();
    }
}
