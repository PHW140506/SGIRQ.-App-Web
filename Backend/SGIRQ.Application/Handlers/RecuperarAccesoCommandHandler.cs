using MediatR;
using SGIRQ.Application.Commands;
using SGIRQ.Application.Interfaces;

namespace SGIRQ.Application.Handlers;

public class SolicitarRecuperacionCommandHandler : IRequestHandler<SolicitarRecuperacionCommand, string>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public SolicitarRecuperacionCommandHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<string> Handle(SolicitarRecuperacionCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObtenerPorIdentificadorAsync(request.Correo, cancellationToken);

        // HU-004 Escenario 3: Respuesta neutral obligatoria
        // Tanto si el usuario existe como si no, se devuelve la misma respuesta genérica
        const string respuestaNeutral = "Si el correo electrónico ingresado está registrado en el sistema, recibirás un enlace de restablecimiento en los próximos minutos.";

        if (usuario is null || !usuario.Activo)
        {
            return respuestaNeutral;
        }

        // Aquí se generaría y enviaría el token temporal por correo
        return respuestaNeutral;
    }
}