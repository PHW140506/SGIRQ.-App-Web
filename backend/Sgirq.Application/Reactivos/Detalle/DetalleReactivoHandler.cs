using Sgirq.Application.Common;
using MediatR;
using Sgirq.Application.Abstractions;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Reactivos.Detalle;
public sealed class DetalleReactivoHandler(IReactivoRepository reactivos, IMapper mapper) : IRequestHandler<DetalleReactivoQuery, ReactivoDto>
{
    public async Task<ReactivoDto> Handle(DetalleReactivoQuery request, CancellationToken ct)
    {
        var r = await reactivos.PorIdAsync(request.Id, ct)
            ?? throw new BusinessException("Reactivo no encontrado", FailureKind.NotFound);
        return mapper.ReactivoDto(r);
    }
}
