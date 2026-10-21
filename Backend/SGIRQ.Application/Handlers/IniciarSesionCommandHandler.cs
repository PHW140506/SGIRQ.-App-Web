using MediatR;
using SGIRQ.Application.Commands;
using SGIRQ.Application.DTOs;
using SGIRQ.Application.Interfaces;
using SGIRQ.Domain.ReglasDeNegocio;

namespace SGIRQ.Application.Handlers;

public class IniciarSesionCommandHandler : IRequestHandler<IniciarSesionCommand, AuthResponseDto>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public IniciarSesionCommandHandler(
        IUsuarioRepository usuarioRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(IniciarSesionCommand request, CancellationToken cancellationToken)
    {
        // 1. Buscar usuario por alias o correo institucional
        var usuario = await _usuarioRepository.ObtenerPorIdentificadorAsync(request.Identificador, cancellationToken);

        // HU-001 Escenario 2: Usuario no encontrado o contraseña incorrecta
        if (usuario is null || !_passwordHasher.VerifyPassword(request.Password, usuario.PasswordHash))
        {
            throw new DomainException("El usuario o la contraseña no son válidos.", "HU-001-ESC2");
        }

        // HU-007 Escenario 2: Cuenta desactivada intenta entrar y se le impide
        if (!usuario.Activo)
        {
            throw new DomainException("Esta cuenta ha sido desactivada. Acceso denegado.", "HU-007-ESC2");
        }

        // HU-001 Escenario 1: Credenciales válidas -> genera JWT
        var token = _jwtTokenGenerator.GenerarToken(usuario);

        return new AuthResponseDto(
            Token: token,
            Usuario: usuario.Username,
            NombreCompleto: usuario.NombreCompleto,
            Rol: usuario.Rol
        );
    }
}