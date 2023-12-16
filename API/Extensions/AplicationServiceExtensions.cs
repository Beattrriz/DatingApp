using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class AplicationServiceExtensions
    {
        public static IServiceCollection AddAplicationService(this IServiceCollection services, IConfiguration config)
        {
            // Add services to the container.
            services.AddDbContext<DataContext>(options =>
              options.UseSqlServer(config.GetConnectionString("ConexaoPadrao"), x => x.UseDateOnlyTimeOnly()));
            //adiciona para configura a requisição do angular a api

            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //mapeamento

            return services;
        }
    }
}