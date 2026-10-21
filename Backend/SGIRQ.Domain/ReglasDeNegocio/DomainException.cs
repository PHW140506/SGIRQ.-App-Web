namespace SGIRQ.Domain.ReglasDeNegocio;

public class DomainException : Exception
{
    public string CodigoHistoriaUsuario { get; }

    public DomainException(string mensaje, string codigoHu = "SGIRQ-DOM") 
        : base(mensaje)
    {
        CodigoHistoriaUsuario = codigoHu;
    }
}