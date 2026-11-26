namespace Sgirq.Application.Abstractions;
public interface IReactivoIdentidad
{
    string NumeroCAS { get; }
    string NombreComercial { get; }
    string NombreQuimico { get; }
    int IdPersonal { get; }
}
