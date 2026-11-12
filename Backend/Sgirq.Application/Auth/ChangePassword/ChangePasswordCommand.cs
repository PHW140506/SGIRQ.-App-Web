using MediatR;
namespace Sgirq.Application.Auth.ChangePassword;
public record ChangePasswordCommand(int IdPersonal, string Actual, string Nueva) : IRequest<Unit>;
