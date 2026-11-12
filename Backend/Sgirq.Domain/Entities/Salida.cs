namespace Sgirq.Domain.Entities;
public sealed class Salida
{
    public int IdSalida { get; set; }
    public DateTime FechaSalida { get; set; }
    public string MotivoSalida { get; set; } = "";
    public int IdUsuario { get; set; }
    public int IdPersonal { get; set; }
    public List<Requiere> Requiere { get; set; } = new();
}
