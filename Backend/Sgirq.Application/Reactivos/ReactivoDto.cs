namespace Sgirq.Application.Reactivos;
public record ReactivoDto(int IdReactivo, string NombreComercial, string NombreQuimico,
    string NumeroCAS, string EstadoFisico, decimal CantidadReactivo,
    string UnidadMedida, decimal StockMinimo, string ColorAnaquel, int IdPersonal);
