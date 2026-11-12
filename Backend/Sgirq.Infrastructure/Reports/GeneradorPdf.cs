using System.Globalization;
using System.Text;
using Sgirq.Application.Abstractions;
namespace Sgirq.Infrastructure.Reports;
// PDF sencillo y legible, sin dependencias adicionales ni lógica de negocio.
public sealed class GeneradorPdf : IGeneradorPdf
{
    public byte[] Generar(string titulo, IReadOnlyList<string> lineas)
    {
        var contenido = new List<string> { titulo, "Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") };
        contenido.AddRange(lineas);
        var paginas = contenido.Chunk(43).ToList();
        var objetos = new List<byte[]>();
        void Agregar(string texto) => objetos.Add(Encoding.Latin1.GetBytes(texto));
        Agregar("<< /Type /Catalog /Pages 2 0 R >>");
        var idsPaginas = Enumerable.Range(0, paginas.Count).Select(i => 4 + i * 2).ToArray();
        Agregar($"<< /Type /Pages /Count {paginas.Count} /Kids [ {string.Join(" ", idsPaginas.Select(id => $"{id} 0 R"))} ] >>");
        Agregar("<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>");
        foreach (var (pagina, indice) in paginas.Select((p, i) => (p, i)))
        {
            var contenidoPdf = new StringBuilder("BT /F1 11 Tf 50 795 Td 15 TL\n");
            foreach (var linea in pagina)
            {
                var limpia = new string(linea.Select(c => c <= 255 ? c : '?').ToArray());
                if (limpia.Length > 105) limpia = limpia[..102] + "...";
                limpia = limpia.Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");
                contenidoPdf.Append('(').Append(limpia).Append(") Tj T*\n");
            }
            contenidoPdf.Append("ET");
            var bytes = Encoding.Latin1.GetBytes(contenidoPdf.ToString());
            Agregar($"<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Resources << /Font << /F1 3 0 R >> >> /Contents {5 + indice * 2} 0 R >>");
            objetos.Add(Encoding.Latin1.GetBytes($"<< /Length {bytes.Length} >>\nstream\n")
                .Concat(bytes).Concat(Encoding.Latin1.GetBytes("\nendstream")).ToArray());
        }
        using var memoria = new MemoryStream();
        void Escribir(string texto) { var bytes = Encoding.Latin1.GetBytes(texto); memoria.Write(bytes); }
        Escribir("%PDF-1.4\n");
        var posiciones = new List<long> { 0 };
        for (var i = 0; i < objetos.Count; i++)
        {
            posiciones.Add(memoria.Position);
            Escribir($"{i + 1} 0 obj\n");
            memoria.Write(objetos[i]);
            Escribir("\nendobj\n");
        }
        var inicioXref = memoria.Position;
        Escribir($"xref\n0 {objetos.Count + 1}\n0000000000 65535 f \n");
        foreach (var posicion in posiciones.Skip(1)) Escribir($"{posicion:0000000000} 00000 n \n");
        Escribir($"trailer << /Size {objetos.Count + 1} /Root 1 0 R >>\nstartxref\n{inicioXref}\n%%EOF");
        return memoria.ToArray();
    }
}
