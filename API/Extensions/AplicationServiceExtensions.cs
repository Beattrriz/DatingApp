using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
             options.UseSqlServer(config.GetConnectionString("ConexaoPadrao")));
            //adiciona para configura a requisição do angular a api

            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}