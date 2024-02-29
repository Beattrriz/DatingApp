using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//aqui criamos o http para realizar as tareafs (enviar para o banco, add, deletar) do lado do front
//apos aqui deve ir para membro.service e inserir o caimnho
//após ir no componento .ts para criar a função que ira realizar o metodo e configurar o html
namespace API.Controllers
{
    [Authorize] //somente usuarios autorizados
    public class UsuariosController: BaseApiController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public readonly IFotoService _fotoService;
        public UsuariosController(IUsuarioRepository usuarioRepository, IMapper mapper,
         IFotoService fotoService) //construtor para criara instancia do banco
        {
            _fotoService = fotoService;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;

        }

    //[AllowAnonymous] permitir anonimato e os atributos autorizados são ignorados
    [HttpGet] //pega
        public async Task<ActionResult<PaginaLista<AppUser>>> GetUsuarios([FromQuery]UsuarioParametro usuarioParametro) //async operação assincrona fromquery da dica a API do que ela deve procurar
        {
            var usuarioAtual = await _usuarioRepository.GetUserByUsernameAsync(User.GetNomeUsuario());// o usuario atual n]ão deve aparecer no Match
            usuarioParametro.UsuarioAtual = usuarioAtual?.Nome;

            if(string.IsNullOrEmpty(usuarioParametro.Genero)){
                usuarioParametro.Genero = usuarioAtual.Genero == "Masculino" ? "Feminino" : "Masculino";
            }
            //O assincronismo é útil para melhorar a experiência do usuário quando há alguma operação que demanda muito tempo para ser executada. Então um cliente pode continuar disponível quando ele pede algo para um serviço que demora. Ele não é usado para tornar algo mais rápido
            var usuarios = await _usuarioRepository.GetMembersAsync(usuarioParametro); // para usar de um jeito assincrono e não sincrono
            Response.AddCabecalhoPaginacao(new CabecalhoPaginacao(usuarios.PaginaAtual, usuarios.TamanhoPagina, 
            usuarios.ContagemTotal,usuarios.TotalPaginas));
            
            return Ok(usuarios);
        }// ação de obter lista de usuarios sem especificar qual esta interessado
     
    [HttpGet("{nome}")]
    public async Task<ActionResult<MembroDto>> GetUsuario(string nome)
    {
          return await _usuarioRepository.GetMemberAsync(nome); //procura pelo nome de usuario
            
        }

    [HttpPut]
    public async Task<ActionResult> UpdateUsuario(MembroUpdateDto membroUpdateDto)
    {
 // esse tem que ser User mesmo vem do sistema
            var user = await _usuarioRepository.GetUserByUsernameAsync(User.GetNomeUsuario());

            if (user == null) return NotFound();

            _mapper.Map(membroUpdateDto, user);

            if (await _usuarioRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Não foi possível atualizar o usuário");
    }

    [HttpPost("add-foto")]

    public async Task<ActionResult<FotoDto>> AddFoto(IFormFile file)
    {
            var usuario = await _usuarioRepository.GetUserByUsernameAsync(User.GetNomeUsuario());

            if (usuario == null) return NotFound();

            var resultado = await _fotoService.AddPhotoAsync(file);

            if (resultado.Error != null) return BadRequest(resultado.Error.Message); //resultado

            //se não der nenhum erro
            var foto = new Foto
            {
                Url = resultado.SecureUrl.AbsoluteUri,
                IdPublico = resultado.PublicId
            };

            if (usuario.Fotos.Count == 0) foto.Principal = true;

            usuario.Fotos.Add(foto);

            if (await _usuarioRepository.SaveAllAsync()) //return _mapper.Map<FotoDto>(foto);
            {
                return CreatedAtAction(nameof(GetUsuario), 
                new { nome = usuario.Nome }, _mapper.Map<FotoDto>(foto));
            }
            return BadRequest("O sistema obteve problemas em adicionar a foto");
    }

    [HttpPut("definir-foto-principal/{IdFoto}")]
    public async Task<ActionResult> DefinirFotoPrincipal(int idFoto)
    {
            var usuario = await _usuarioRepository.GetUserByUsernameAsync(User.GetNomeUsuario());

            if (usuario == null) return NotFound();

            var foto = usuario.Fotos.FirstOrDefault(x => x.Id == idFoto);

            if (foto == null) return NotFound();

            if (foto.Principal) return BadRequest("Esta já é sua foto principal");

            var fotoAtual = usuario.Fotos.FirstOrDefault(x => x.Principal);
            if (fotoAtual != null) fotoAtual.Principal = false;
            foto.Principal = true;

            if (await _usuarioRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Problema ao configurar a foto principal");
    }

    [HttpDelete("deletar-foto/{IdFoto}")]
    public async Task<ActionResult> DeletarFoto(int idFoto){

            var usuario = await _usuarioRepository.GetUserByUsernameAsync(User.GetNomeUsuario());

            var foto = usuario.Fotos.FirstOrDefault(x => x.Id == idFoto); //acessa as fotos

            if (foto == null) return NotFound();

            if (foto.Principal) return BadRequest("Você não pode apagar sua foto principal");//nao pode apagar a foto principal

            if(foto.IdPublico != null)
            {
                var result = await _fotoService.DeletePhotoAsync(foto.IdPublico);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }//se a foto tem identificação publica

            usuario.Fotos.Remove(foto);

            if (await _usuarioRepository.SaveAllAsync()) return Ok();

            return BadRequest("Houve problemas em deletar a foto");
    }


    }   

}

   