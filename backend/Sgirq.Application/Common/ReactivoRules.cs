using Sgirq.Application.Abstractions;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Common;
public sealed class ReactivoRules(IReactivoRepository reactivos, IPersonalRepository personal) : IReactivoRules
{
    public async Task ComprobarAsync(IReactivoIdentidad x, int? excepto, CancellationToken ct)
    {
        if (await reactivos.CasOcupadoAsync(x.NumeroCAS.Trim(), excepto, ct))
            throw new BusinessException("El número CAS ya está registrado", FailureKind.Conflict);
        if (await reactivos.NombreComercialOcupadoAsync(x.NombreComercial.Trim(), excepto, ct))
            throw new BusinessException("El nombre comercial ya está registrado", FailureKind.Conflict);
        if (await reactivos.NombreQuimicoOcupadoAsync(x.NombreQuimico.Trim(), excepto, ct))
            throw new BusinessException("El nombre químico ya está registrado", FailureKind.Conflict);
        if (!await personal.ExisteAsync(x.IdPersonal, ct))
            throw new BusinessException("El responsable no existe", FailureKind.NotFound);
    }
}
