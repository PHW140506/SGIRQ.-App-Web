using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Sgirq.Api.Extensions;
using Sgirq.Api.Middleware;
using Sgirq.Api.Security;
using Sgirq.Application;
using Sgirq.Application.Abstractions;
using Sgirq.Infrastructure;
var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrWhiteSpace(jwtKey) || Encoding.UTF8.GetByteCount(jwtKey) < 32)
    throw new InvalidOperationException("Configura Jwt:Key (mínimo 32 bytes) en User Secrets");
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ITokens, JwtTokens>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = false, ValidateAudience = false, ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.FromMinutes(1)
    });
builder.Services.AddAuthorization();
builder.Services.AddCors(o => o.AddPolicy("Angular", p => p
    .WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));
var app = builder.Build();
app.UseMiddleware<ApiErrorsMiddleware>();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseCors("Angular");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.SeedDevelopmentAsync();
app.Run();
