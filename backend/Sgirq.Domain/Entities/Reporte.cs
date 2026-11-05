namespace Sgirq.Domain.Entities;
public sealed class Reporte
{
    public int IdReporte { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFinal { get; set; }
    public string TipoReporte { get; set; } = "";
    public int IdPersonal { get; set; }
}
