using System.Globalization;
using System.Text;
using Sgirq.Application.Abstractions;
namespace Sgirq.Infrastructure.Reports;
public sealed class GeneradorPdf : IGeneradorPdf
{
    private static byte[] Bytes(string s) => Encoding.Latin1.GetBytes(s);
    private static string Escape(string s) => new string(s.Select(c => c <= 255 ? c : '?').ToArray())
        .Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");

    public byte[] Generar(string titulo, DateTime? desde, DateTime? hasta, IReadOnlyList<string> lineas)
    {
        var logoPath = Path.Combine(AppContext.BaseDirectory, "logo-sgirq.jpeg");
        var logo = File.Exists(logoPath) ? File.ReadAllBytes(logoPath) : Array.Empty<byte>();
        var contenido = lineas.Count == 0 ? new[] { "Sin registros para los filtros seleccionados." } : lineas;
        var paginas = contenido.Chunk(32).ToArray();
        if (paginas.Length == 0) paginas = [Array.Empty<string>()];
        var objetos = new List<byte[]>();
        void Add(string data) => objetos.Add(Bytes(data));
        Add("<< /Type /Catalog /Pages 2 0 R >>");
        var paginaIds = Enumerable.Range(0, paginas.Length).Select(i => 5 + i * 2).ToArray();
        Add($"<< /Type /Pages /Count {paginas.Length} /Kids [{string.Join(" ", paginaIds.Select(id => $"{id} 0 R"))}] >>");
        Add("<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>");
        if (logo.Length > 0)
        {
            var prefix = Bytes($"<< /Type /XObject /Subtype /Image /Width 1024 /Height 1024 /ColorSpace /DeviceRGB /BitsPerComponent 8 /Filter /DCTDecode /Length {logo.Length} >>\nstream\n");
            objetos.Add(prefix.Concat(logo).Concat(Bytes("\nendstream")).ToArray());
        }
        else Add("<< >>");
        for (var i = 0; i < paginas.Length; i++)
        {
            var builder = new StringBuilder();
            builder.Append("0.08 0.30 0.24 rg 0 750 595 92 re f\n");
            if (logo.Length > 0) builder.Append("q 72 0 0 72 35 760 cm /Im1 Do Q\n");
            builder.Append("1 1 1 rg BT /F1 17 Tf 125 806 Td (SGIRQ) Tj ET\n");
            builder.Append("BT /F1 10 Tf 125 785 Td (Sistema de Gestion de Inventario de Reactivos Quimicos) Tj ET\n");
            builder.Append("0 0 0 rg BT /F1 13 Tf 44 725 Td (").Append(Escape(titulo)).Append(") Tj ET\n");
            var periodo = desde.HasValue || hasta.HasValue ? $"Periodo: {desde:dd/MM/yyyy} - {hasta:dd/MM/yyyy}" : "Periodo: inventario vigente";
            builder.Append("BT /F1 10 Tf 44 704 Td (").Append(Escape(periodo)).Append(") Tj ET\n");
            builder.Append("BT /F1 9 Tf 44 687 Td (Generado: ").Append(Escape(DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture))).Append(") Tj ET\n");
            builder.Append("0.89 0.94 0.91 rg 40 655 515 23 re f\n0 0 0 rg BT /F1 10 Tf 47 663 Td (Detalle de reactivos y movimientos) Tj ET\n");
            var y = 634;
            foreach (var (linea, indice) in paginas[i].Select((linea, indice) => (linea, indice)))
            {
                if (indice % 2 == 0) builder.Append($"0.96 0.97 0.96 rg 40 {y - 7} 515 18 re f\n");
                var texto = linea.Length > 105 ? linea[..102] + "..." : linea;
                builder.Append($"0 0 0 rg BT /F1 9 Tf 46 {y} Td ({Escape(texto)}) Tj ET\n");
                y -= 18;
            }
            builder.Append($"0.35 0.35 0.35 rg BT /F1 9 Tf 45 35 Td (Pagina {i + 1} de {paginas.Length}) Tj ET\n");
            var buffer = Bytes(builder.ToString());
            var resources = logo.Length > 0 ? "/XObject << /Im1 4 0 R >>" : "";
            Add($"<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Resources << /Font << /F1 3 0 R >> {resources} >> /Contents {6 + i * 2} 0 R >>");
            objetos.Add(Bytes($"<< /Length {buffer.Length} >>\nstream\n").Concat(buffer).Concat(Bytes("\nendstream")).ToArray());
        }
        using var result = new MemoryStream();
        void Put(string value) => result.Write(Bytes(value));
        Put("%PDF-1.4\n");
        var offsets = new List<long> { 0 };
        for (var i = 0; i < objetos.Count; i++)
        {
            offsets.Add(result.Position);
            Put($"{i + 1} 0 obj\n"); result.Write(objetos[i]); Put("\nendobj\n");
        }
        var xref = result.Position;
        Put($"xref\n0 {objetos.Count + 1}\n0000000000 65535 f \n");
        foreach (var offset in offsets.Skip(1)) Put($"{offset:0000000000} 00000 n \n");
        Put($"trailer << /Size {objetos.Count + 1} /Root 1 0 R >>\nstartxref\n{xref}\n%%EOF");
        return result.ToArray();
    }
}
