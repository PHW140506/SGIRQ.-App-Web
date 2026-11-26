namespace Sgirq.Application.Abstractions;
public interface IGeneradorPdf
{
    byte[] Generar(string titulo, DateTime? desde, DateTime? hasta, IReadOnlyList<string> lineas);
}
