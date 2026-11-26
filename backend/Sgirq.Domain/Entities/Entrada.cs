namespace Sgirq.Domain.Entities;
public sealed class Entrada
{
    public int IdEntrada { get; set; }
    public int IdReactivo { get; set; }
    public decimal Cantidad { get; set; }
    public DateTime FechaEntrada { get; set; }
}
