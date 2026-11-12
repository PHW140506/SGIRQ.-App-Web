using MediatR;
using Sgirq.Application.Salidas.Listar;
namespace Sgirq.Application.Solicitudes.Consultar;
public record ConsultarSolicitudesQuery(int IdUsuario) : IRequest<List<SalidaDto>>;
