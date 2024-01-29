using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize] //somente usuarios autorizados
    public class UsuariosController: BaseApiController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepository usuarioRepository, IMapper mapper) //construtor para criara instancia do banco
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;

        }

    //[AllowAnonymous] permitir anonimato e os atributos autorizados são ignorados
    [HttpGet] //pega
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsuarios() //async operação assincrona
        {
            //O assincronismo é útil para melhorar a experiência do usuário quando há alguma operação que demanda muito tempo para ser executada. Então um cliente pode continuar disponível quando ele pede algo para um serviço que demora. Ele não é usado para tornar algo mais rápido
            var usuarios = await _usuarioRepository.GetMembersAsync(); // para usar de um jeito assincrono e não sincrono

            return Ok(usuarios);
        }// ação de obter lista de usuarios sem especificar qual esta interessado
     
    [HttpGet("{nome}")]
    public async Task<ActionResult<MembroDto>> GetUsuarios(string nome)
    {
          return await _usuarioRepository.GetMemberAsync(nome); //procura pelo nome de usuario
            
        }

    [HttpPut]
    public async Task<ActionResult> UpdateUsuario(MembroUpdateDto membroUpdateDto)
    {
           var nome = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // esse tem que ser User mesmo vem do sistema
            var user = await _usuarioRepository.GetUserByUsernameAsync(nome);

            if (user == null) return NotFound();

            _mapper.Map(membroUpdateDto, user);

            if (await _usuarioRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Não foi possível atualizar o usuário");
    }

    }   

}

   