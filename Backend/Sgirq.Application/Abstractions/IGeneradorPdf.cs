namespace Sgirq.Application.Abstractions;
public interface IGeneradorPdf
{
    byte[] Generar(string titulo, IReadOnlyList<string> lineas);
}
