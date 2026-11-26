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
    public void Ingresar(decimal cantidad)
    {
        if (cantidad <= 0 || CantidadReactivo + cantidad > 99999999.99m)
            throw new Sgirq.Domain.Rules.InventarioException("Cantidad fuera del rango permitido");
        CantidadReactivo += cantidad;
    }

    public void Retirar(decimal cantidad)
    {
        if (cantidad <= 0 || CantidadReactivo < cantidad)
            throw new Sgirq.Domain.Rules.InventarioException($"Sin existencias suficientes: {NombreComercial}");
        CantidadReactivo -= cantidad;
    }
}
