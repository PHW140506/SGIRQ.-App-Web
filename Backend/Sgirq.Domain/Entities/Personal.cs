namespace Sgirq.Domain.Entities;
public sealed class Personal
{
    public int IdPersonal { get; set; }
    public string NombrePersonal { get; set; } = "";
    public string ApellidosPersonal { get; set; } = "";
    public string UsuarioPersonalLogin { get; set; } = "";
    public string Contrasenia { get; set; } = "";
    public string TipoPersonal { get; set; } = "";
}
