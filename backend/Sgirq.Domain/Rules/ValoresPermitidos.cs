namespace Sgirq.Domain.Rules;
public static class ValoresPermitidos
{
    public static readonly string[] Roles = ["Administrador", "Auxiliar", "Solicitante"];
    public static readonly string[] Estados = ["Líquido", "Sólido"];
    public static readonly string[] Unidades = ["Mililitro", "Gramo", "Litro", "Kilogramo"];
    public static readonly string[] Anaqueles = ["Verde", "Rojo", "Azul", "Amarillo", "Blanco", "Naranja"];
}
