using FluentValidation;
namespace Sgirq.Application.Auth.Login;
public sealed class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Usuario).NotEmpty();
        RuleFor(x => x.Contrasenia).NotEmpty();
    }
}
