using MediatR;
namespace Sgirq.Application.Auth.Me;
public record MeQuery(int IdPersonal) : IRequest<MeResult?>;
public record MeResult(int IdPersonal, string Nombre, string Usuario, string Rol);
