using Sgirq.Domain.Entities;
namespace Sgirq.Application.Abstractions;
public interface ITokens
{
    string Generate(Sgirq.Domain.Entities.Personal personal);
}
