using MediatR;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
using Sgirq.Domain.Entities;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Reportes.Generar;
public sealed class GenerarReporteHandler(IReactivoRepository reactivos, ISalidaRepository salidas,
    IReporteRepository reportes, IPersonalRepository personal, IUnidadDeTrabajo unidad, IGeneradorPdf pdf)
    : IRequestHandler<GenerarReporteCommand, byte[]>
{
    public async Task<byte[]> Handle(GenerarReporteCommand x, CancellationToken ct)
    {
        if (!await personal.ExisteAsync(x.IdPersonal, ct))
            throw new BusinessException("Responsable inexistente", 404);
        var inventario = await reactivos.ListarAsync(null, null, null, ct);
        var lineas = new List<string>();
        if (x.Tipo == "inventario" || x.Tipo == "bajo-stock")
        {
            var filtrado = x.Tipo == "bajo-stock"
                ? inventario.Where(r => r.CantidadReactivo <= r.StockMinimo) : inventario;
            foreach (var r in filtrado)
                lineas.Add($"{r.IdReactivo} | {r.NombreComercial} | CAS {r.NumeroCAS} | {r.CantidadReactivo} {r.UnidadMedida} | Min {r.StockMinimo} | {r.ColorAnaquel}");
        }
        else
        {
            var registros = await salidas.ListarAsync(x.Desde, x.Hasta, null, null, ct);
            var nombres = inventario.ToDictionary(r => r.IdReactivo, r => r.NombreComercial);
            if (x.Tipo == "salidas")
            {
                foreach (var s in registros)
                {
                    lineas.Add($"Salida {s.IdSalida} | {s.FechaSalida:dd/MM/yyyy} | Usuario {s.IdUsuario} | Personal {s.IdPersonal} | {s.MotivoSalida}");
                    lineas.AddRange(s.Requiere.Select(i => $"   {nombres.GetValueOrDefault(i.IdReactivo, "Reactivo")}: {i.CantidadSolicitada}"));
                }
            }
            else
            {
                foreach (var grupo in registros.SelectMany(s => s.Requiere).GroupBy(i => i.IdReactivo))
                    lineas.Add($"{nombres.GetValueOrDefault(grupo.Key, "Reactivo")} | Total solicitado {grupo.Sum(i => i.CantidadSolicitada)}");
            }
        }
        if (lineas.Count == 0) lineas.Add("No hay datos en el periodo seleccionado.");
        var inicio = x.Desde ?? DateTime.Today;
        var final = x.Hasta ?? DateTime.Today;
        await reportes.AgregarAsync(new Reporte {
            FechaInicio = inicio, FechaFinal = final, TipoReporte = x.Tipo,
            IdPersonal = x.IdPersonal
        }, ct);
        await unidad.GuardarAsync(ct);
        return pdf.Generar("SGIRQ - Reporte " + x.Tipo, lineas);
    }
}
