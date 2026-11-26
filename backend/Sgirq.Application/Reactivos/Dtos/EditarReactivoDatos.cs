namespace Sgirq.Application.Reactivos.Editar;
public sealed record EditarReactivoDatos(string NombreComercial, string NombreQuimico,
    string NumeroCAS, string EstadoFisico, string UnidadMedida, decimal StockMinimo,
    string ColorAnaquel, int IdPersonal) : Sgirq.Application.Abstractions.IReactivoIdentidad;
