using MediatR;
namespace Sgirq.Application.Reactivos.Editar;
// La cantidad NO se edita aquí; únicamente cambia con entradas y salidas.
public record EditarReactivoCommand(int IdReactivo, string NombreComercial,
    string NombreQuimico, string NumeroCAS, string EstadoFisico, string UnidadMedida,
    decimal StockMinimo, string ColorAnaquel, int IdPersonal) : IRequest<Unit>;
