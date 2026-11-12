using MediatR;
namespace Sgirq.Application.Reactivos.Crear;
public record CrearReactivoCommand(
    string NombreComercial, 
    string NombreQuimico,
    string NumeroCAS,
    string EstadoFisico, 
    decimal CantidadReactivo, 
    string UnidadMedida,
    decimal StockMinimo, 
    string ColorAnaquel, int IdPersonal) : IRequest<int>;
