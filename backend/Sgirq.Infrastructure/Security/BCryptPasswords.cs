using Sgirq.Application.Abstractions;
namespace Sgirq.Infrastructure.Security;
public sealed class BCryptPasswords : IPasswords
{
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
}
