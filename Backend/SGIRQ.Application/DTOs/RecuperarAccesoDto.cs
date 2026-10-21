namespace SGIRQ.Application.DTOs;

public record SolicitarRecuperacionDto(string Correo);

public record RestablecerPasswordDto(string Token, string NuevaPassword);