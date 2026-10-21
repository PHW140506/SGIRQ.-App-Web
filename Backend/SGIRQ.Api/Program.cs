using SGIRQ.Api.ConfiguracionDI;
using SGIRQ.Application.Interfaces;
using SGIRQ.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSgirqDependencies(builder.Configuration);

var app = builder.Build();

// Inicializar base de datos y usuario semilla
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SgirqDbContext>();
    var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
    await DbInitializer.InicializarAsync(db, hasher);
}

app.UseCors("AngularClientPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();