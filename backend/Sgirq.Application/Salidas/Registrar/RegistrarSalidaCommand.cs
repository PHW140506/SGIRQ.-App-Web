using MediatR;
namespace Sgirq.Application.Salidas.Registrar;
public record SalidaItem(int IdReactivo, decimal CantidadSolicitada);
// IdSalida es generado una sola vez por el cliente para detectar reenvíos sin añadir columnas.
public record RegistrarSalidaCommand(int IdSalida, DateTime FechaSalida, string MotivoSalida,
    int IdUsuario, int IdPersonal, List<SalidaItem> Reactivos) : IRequest<int>;
