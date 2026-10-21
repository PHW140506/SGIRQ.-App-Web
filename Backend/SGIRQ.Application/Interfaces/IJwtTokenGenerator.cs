using SGIRQ.Domain.Entities;

namespace SGIRQ.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerarToken(Usuario usuario);
}