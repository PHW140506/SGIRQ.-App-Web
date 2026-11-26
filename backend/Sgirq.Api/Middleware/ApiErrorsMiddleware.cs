using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Sgirq.Application.Common;
namespace Sgirq.Api.Middleware;
public sealed class ApiErrorsMiddleware(RequestDelegate next, ILogger<ApiErrorsMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        try { await next(ctx); }
        catch (BusinessException ex)
        {
            ctx.Response.StatusCode = ex.Kind switch
            {
                FailureKind.Validation => StatusCodes.Status400BadRequest,
                FailureKind.Unauthorized => StatusCodes.Status401Unauthorized,
                FailureKind.NotFound => StatusCodes.Status404NotFound,
                FailureKind.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
            await ctx.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
        catch (Sgirq.Domain.Rules.InventarioException ex)
        {
            ctx.Response.StatusCode = 409;
            await ctx.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
        catch (DbUpdateException ex) when (ex.InnerException is MySqlException mysql && mysql.Number == 1062)
        {
            ctx.Response.StatusCode = 409;
            await ctx.Response.WriteAsJsonAsync(new { message = "Registro duplicado. Comprueba si ya se guardó." });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error inesperado de API");
            ctx.Response.StatusCode = 500;
            await ctx.Response.WriteAsJsonAsync(new { message = "Ocurrió un error al procesar la operación" });
        }
    }
}
