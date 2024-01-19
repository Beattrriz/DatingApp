using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DadosIniciais
    {
        public static async Task DadosIniciaisUsuario(DataContext context)
        {
            if (await context.Usuarios.AnyAsync()) return;

            var dadoUsuario = await File.ReadAllTextAsync("Data/DadosIniciaisUsuario.json"); //verificar os usuários

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var usuarios = JsonSerializer.Deserialize<List<AppUser>>(dadoUsuario); //options como segundo parametro

            foreach(var usuario in usuarios)
            {
                using var hmac = new HMACSHA512();

                usuario.Nome = usuario.Nome.ToLower();
                usuario.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")); //senha usuarios
                usuario.PasswordSalt = hmac.Key;

                context.Usuarios.Add(usuario);
            }

            await context.SaveChangesAsync();
        }
    }
}