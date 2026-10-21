# SGIRQ - Sistema de Gestión de Inventario de Reactivos Químicos

Sistema para el control y administración de reactivos químicos (Almacén K - UTSJR).

## Stack Tecnológico
- **Backend:** .NET 8 / 9 Web API (Clean Architecture, CQRS con MediatR)
- **Frontend:** Angular (Standalone Components, Tailwind CSS)
- **Base de Datos:** SQLite / SQL Server con Entity Framework Core

## Flujo de Trabajo Git (Sprint 1)
- **Rama activa de desarrollo:** `sp1`
- **Convención de ramas de trabajo:** `task/HU-XXX-nombre-tarea`
- **Integración:** Todo cambio se integra a `sp1` mediante Pull Request con mínimo 1 aprobación.

## Ejecución Local

### Backend
```bash
cd Backend/SGIRQ.Api
dotnet run