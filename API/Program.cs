using API.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using API.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Extensions;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 //adiciona para configura a requisição do angular a api

builder.Services.AddControllers();
builder.Services.AddAplicationService(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("https://localhost:4200"));
// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();
//cabeçalho da aplicação angula api e qualquer metodo

app.UseAuthentication(); //autenticam o pedido
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope(); //tem que colocar exatamente aqui
var services = scope.ServiceProvider;
try{
    var context = services.GetRequiredService<DataContext>(); //acessa o banco para criar migrations
    await context.Database.MigrateAsync();
    await DadosIniciais.DadosIniciaisUsuario(context);
}
catch(Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "Ocorreu um erro durante a migração");
}

app.Run();
