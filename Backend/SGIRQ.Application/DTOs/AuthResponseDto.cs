namespace SGIRQ.Application.DTOs;

public record AuthResponseDto(
    string Token,
    string Usuario,
    string NombreCompleto,
    string Rol
);