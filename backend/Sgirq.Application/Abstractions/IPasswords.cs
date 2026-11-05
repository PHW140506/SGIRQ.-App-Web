namespace Sgirq.Application.Abstractions;
public interface IPasswords
{
    string Hash(string password);
    bool Verify(string password, string hash);
}
