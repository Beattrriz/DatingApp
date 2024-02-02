using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using API.Interfaces;
using API.Services;

namespace API.Controllers
{
    public class ContasController :BaseApiController 
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public ContasController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;


        }

        [HttpPost("registro")] //api/contas/registro?nomerusuario=dave&senha=pwd
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
        {
            if(await ExisteUsuario(registroDto.NomeUsuario)) return BadRequest("Nome de Usuário em uso");
            
            using var hmac = new HMACSHA512(); //USING DESCARTA vai descartar o hashin
            
            var usuario = new AppUser //cria um novo usuario
            {
                Nome = registroDto.NomeUsuario.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Senha)), //calcula o algoritmo de hash
                PasswordSalt = hmac.Key //cgave gerada aleatoria
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDto
            {
                NomeUsuario = usuario.Nome,
                Token = _tokenService.CreateToken(usuario)          
            };
        }

        //faz o inverso para pegar o login do usuario e verificar
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _context.Usuarios
            .Include(f => f.Fotos)
            .SingleOrDefaultAsync(x => x.Nome == loginDto.NomeUsuario);
            
            if(usuario == null) return Unauthorized("Nome de Usuário inválido");

            using var hmac = new HMACSHA512(usuario.PasswordSalt);

            var HashComputado = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Senha));

            for(int i = 0; i < HashComputado.Length; i++)
            {
                if(HashComputado[i] != usuario.PasswordHash[i]) return Unauthorized("Senha inválida");
            }

            return new UsuarioDto
            {
                NomeUsuario = usuario.Nome,
                Token = _tokenService.CreateToken(usuario),
                FotoUrl = usuario.Fotos.FirstOrDefault(x => x.Principal)?.Url //a foto naõ aparece pq estava vazia, naõ tem foto para verificar
                //vai alterar o httpost login
            };
        }
        //verifica se existe o usuario
        private async Task<bool> ExisteUsuario(string nomeusuario)
        {
            return await _context.Usuarios.AnyAsync(x => x.Nome == nomeusuario.ToLower());
        }
    }
}