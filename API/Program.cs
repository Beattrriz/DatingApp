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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*builder.Services.AddDbContext<DataContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));*/
 
 //adiciona para configura a requisição do angular a api

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddCors();
//builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAplicationService(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(Options =>
{ //validação token
    Options.TokenValidationParameters = new TokenValidationParameters
    { //verifica se a chave de assinatura é válida
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding
        .UTF8.GetBytes(builder.Configuration["TokenKey"])), //chave de assinatura
        ValidateIssuer = false,
        ValidateAudience = false
    };
});*/


var app = builder.Build();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("https://localhost:4200"));
// Configure the HTTP request pipeline.
//cabeçalho da aplicação angula api e qualquer metodo

app.UseAuthentication(); //autenticam o pedido
app.UseAuthorization();

app.MapControllers();

app.Run();
