using MediatR;
namespace Sgirq.Application.Salidas.Listar;
public record ListarSalidasQuery(DateTime? Desde, DateTime? Hasta) : IRequest<List<SalidaDto>>;
public record ItemSalidaDto(int IdReactivo, string Reactivo, decimal CantidadSolicitada);
public record SalidaDto(int IdSalida, DateTime FechaSalida, string MotivoSalida,
    int IdUsuario, int IdPersonal, string Responsable, List<ItemSalidaDto> Reactivos);
