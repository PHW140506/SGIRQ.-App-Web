using FluentValidation;
namespace Sgirq.Application.Auth.ChangePassword;
public sealed class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.Actual).NotEmpty();
        RuleFor(x => x.Nueva).NotEmpty().MinimumLength(10);
    }
}
