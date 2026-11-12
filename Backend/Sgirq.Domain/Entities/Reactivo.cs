namespace Sgirq.Domain.Entities;
public sealed class Reactivo
{
    public int IdReactivo { get; set; }
    public string NombreComercial { get; set; } = "";
    public string NombreQuimico { get; set; } = "";
    public string NumeroCAS { get; set; } = "";
    public string EstadoFisico { get; set; } = "";
    public decimal CantidadReactivo { get; set; }
    public string UnidadMedida { get; set; } = "";
    public decimal StockMinimo { get; set; }
    public string ColorAnaquel { get; set; } = "";
    public int IdPersonal { get; set; }
}
