using Microsoft.EntityFrameworkCore;
using SGIRQ.Application.Interfaces;
using SGIRQ.Domain.Entities;
using SGIRQ.Infrastructure.Persistence;

namespace SGIRQ.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly SgirqDbContext _context;

    public UsuarioRepository(SgirqDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObtenerPorIdentificadorAsync(string identificador, CancellationToken ct = default)
    {
        var limpio = identificador.Trim().ToLower();
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Username == limpio || u.Correo == limpio, ct);
    }

    public async Task AgregarAsync(Usuario usuario, CancellationToken ct = default)
    {
        await _context.Usuarios.AddAsync(usuario, ct);
    }

    public async Task GuardarCambiosAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}