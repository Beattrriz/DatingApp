using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //acessar 
    public class UsuariosController
    : ControllerBase
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context) //construtor para criara instancia do banco
        {
            _context = context;
        }
    [HttpGet] //pega
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsuarios() //async operação assincrona
        {
            //O assincronismo é útil para melhorar a experiência do usuário quando há alguma operação que demanda muito tempo para ser executada. Então um cliente pode continuar disponível quando ele pede algo para um serviço que demora. Ele não é usado para tornar algo mais rápido
            var usuarios = await _context.Usuarios.ToListAsync(); // para usar de um jeito assincrono e não sincrono
            return usuarios;
        }// ação de obter lista de usuarios sem especificar qual esta interessado
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUsuarios(int id)
    {
            return await _context.Usuarios.FindAsync(id);
            
        }
    }

}