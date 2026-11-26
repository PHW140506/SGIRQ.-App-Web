using MediatR;
using Sgirq.Application.Common;
using Sgirq.Domain.Entities;
using Sgirq.Domain.Repositories;
using Sgirq.Application.Abstractions;
namespace Sgirq.Application.Reportes.Generar;
public sealed class GenerarReporteHandler(IReactivoRepository reactivos, IEntradaRepository entradas,
    ISalidaRepository salidas, IPersonalRepository personal, IReporteRepository reportes,
    IUnidadDeTrabajo unidad, IGeneradorPdf pdf) : IRequestHandler<GenerarReporteCommand, byte[]>
{
    public async Task<byte[]> Handle(GenerarReporteCommand comando, CancellationToken ct)
    {
        var lineas = await ObtenerContenidoAsync(comando, ct);
        var archivo = pdf.Generar("Reporte: " + comando.Tipo, comando.Desde, comando.Hasta, lineas);
        await GuardarRegistroAsync(comando, ct);
        return archivo;
    }

    private async Task<IReadOnlyList<string>> ObtenerContenidoAsync(GenerarReporteCommand filtro, CancellationToken ct)
    {
        var lineas = filtro.Tipo switch
        {
            "inventario" => await InventarioAsync(filtro, ct),
            "bajo-stock" => await BajoStockAsync(filtro, ct),
            "entradas" => await EntradasAsync(filtro, ct),
            "salidas" => await SalidasAsync(filtro, ct),
            "consumo" => await ConsumoAsync(filtro, ct),
            _ => throw new BusinessException("Tipo de reporte inválido")
        };
        return lineas.Count == 0 ? ["No hay datos en el periodo seleccionado."] : lineas;
    }

    private async Task<IReadOnlyList<string>> InventarioAsync(GenerarReporteCommand filtro, CancellationToken ct) =>
        (await reactivos.ListarAsync(null, null, null, ct)).Select(r =>
            $"{r.IdReactivo} | {r.NombreComercial} | CAS {r.NumeroCAS} | {r.CantidadReactivo} {r.UnidadMedida} | Min {r.StockMinimo} | {r.ColorAnaquel}").ToList();

    private async Task<IReadOnlyList<string>> BajoStockAsync(GenerarReporteCommand filtro, CancellationToken ct) =>
        (await reactivos.ListarAsync(null, null, null, ct))
        .Where(r => r.CantidadReactivo <= r.StockMinimo)
        .Select(r => $"{r.IdReactivo} | {r.NombreComercial} | CAS {r.NumeroCAS} | {r.CantidadReactivo} {r.UnidadMedida} | Min {r.StockMinimo} | {r.ColorAnaquel}").ToList();

    private async Task<IReadOnlyList<string>> EntradasAsync(GenerarReporteCommand filtro, CancellationToken ct)
    {
        var nombres = (await reactivos.ListarAsync(null, null, null, ct)).ToDictionary(r => r.IdReactivo);
        var registros = await entradas.ListarAsync(filtro.Desde, filtro.Hasta, ct);
        return registros.Where(e => nombres.ContainsKey(e.IdReactivo)).Select(e =>
        {
            var r = nombres[e.IdReactivo];
            return $"{e.FechaEntrada:dd/MM/yyyy HH:mm} | {r.NombreComercial} | CAS {r.NumeroCAS} | +{e.Cantidad} {r.UnidadMedida}";
        }).ToList();
    }

    private async Task<IReadOnlyList<string>> SalidasAsync(GenerarReporteCommand filtro, CancellationToken ct)
    {
        var nombres = (await reactivos.ListarAsync(null, null, null, ct))
            .ToDictionary(r => r.IdReactivo, r => r.NombreComercial);
        var registros = await salidas.ListarAsync(filtro.Desde, filtro.Hasta, null, null, ct);
        var lineas = new List<string>();
        foreach (var salida in registros)
        {
            lineas.Add($"Salida {salida.IdSalida} | {salida.FechaSalida:dd/MM/yyyy} | Usuario {salida.IdUsuario} | Personal {salida.IdPersonal} | {salida.MotivoSalida}");
            lineas.AddRange(salida.Requiere.Select(item =>
                $"   {nombres.GetValueOrDefault(item.IdReactivo, "Reactivo")}: {item.CantidadSolicitada}"));
        }
        return lineas;
    }

    private async Task<IReadOnlyList<string>> ConsumoAsync(GenerarReporteCommand filtro, CancellationToken ct)
    {
        var nombres = (await reactivos.ListarAsync(null, null, null, ct))
            .ToDictionary(r => r.IdReactivo, r => r.NombreComercial);
        var registros = await salidas.ListarAsync(filtro.Desde, filtro.Hasta, null, null, ct);
        return registros.SelectMany(s => s.Requiere).GroupBy(i => i.IdReactivo)
            .Select(g => $"{nombres.GetValueOrDefault(g.Key, "Reactivo")} | Total solicitado {g.Sum(i => i.CantidadSolicitada)}")
            .ToList();
    }

    private async Task GuardarRegistroAsync(GenerarReporteCommand comando, CancellationToken ct)
    {
        if (!await personal.ExisteAsync(comando.IdPersonal, ct))
            throw new BusinessException("Responsable inexistente", FailureKind.NotFound);
        await reportes.AgregarAsync(new Reporte
        {
            FechaInicio = comando.Desde ?? DateTime.Today,
            FechaFinal = comando.Hasta ?? DateTime.Today,
            TipoReporte = comando.Tipo,
            IdPersonal = comando.IdPersonal
        }, ct);
        await unidad.GuardarAsync(ct);
    }
}
