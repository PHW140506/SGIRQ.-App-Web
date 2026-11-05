using FluentValidation;
using MediatR;
namespace Sgirq.Application.Common;
public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        foreach (var validator in validators)
        {
            var result = await validator.ValidateAsync(request, ct);
            if (!result.IsValid)
                throw new BusinessException(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));
        }
        return await next();
    }
}
