namespace SGIRQ.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string NombreCompleto { get; private set; } = string.Empty;
    public string Username { get; private set; } = string.Empty;
    public string Correo { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Rol { get; private set; } = string.Empty; // Administrador, Auxiliar, etc.
    public bool Activo { get; private set; } = true;

    private Usuario() { }

    public Usuario(string nombreCompleto, string username, string correo, string passwordHash, string rol)
    {
        NombreCompleto = nombreCompleto;
        Username = username.Trim().ToLower();
        Correo = correo.Trim().ToLower();
        PasswordHash = passwordHash;
        Rol = rol;
        Activo = true;
    }

    public void Desactivar() => Activo = false;
    public void Activar() => Activo = true;
}