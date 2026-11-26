namespace Sgirq.Domain.Entities;
public sealed class Salida
{
    public int IdSalida { get; set; }
    public DateTime FechaSalida { get; set; }
    public string MotivoSalida { get; set; } = "";
    public int IdUsuario { get; set; }
    public int IdPersonal { get; set; }
    public List<Requiere> Requiere { get; set; } = new();

    public bool MismoContenido(Salida otra) =>
        IdUsuario == otra.IdUsuario && IdPersonal == otra.IdPersonal &&
        MotivoSalida.Trim() == otra.MotivoSalida.Trim() && FechaSalida == otra.FechaSalida &&
        Requiere.Count == otra.Requiere.Count &&
        Requiere.All(item => otra.Requiere.Any(otro =>
            item.IdReactivo == otro.IdReactivo &&
            item.CantidadSolicitada == otro.CantidadSolicitada));
}
