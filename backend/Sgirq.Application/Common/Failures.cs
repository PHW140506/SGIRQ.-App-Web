namespace Sgirq.Application.Common;
public sealed class BusinessException(string message, FailureKind kind = FailureKind.Validation) : Exception(message)
{
    public FailureKind Kind { get; } = kind;
}
