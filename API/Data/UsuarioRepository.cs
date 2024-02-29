using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public readonly DataContext _context;
        public readonly IMapper _mapper;
        public UsuarioRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }
        public async Task<MembroDto> GetMemberAsync(string nome)
        {
            return await _context.Usuarios
            .Where(x => x.Nome == nome)
            .ProjectTo<MembroDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<PaginaLista<MembroDto>> GetMembersAsync(UsuarioParametro usuarioParametro) //metodo que vai retornar os parametros para paginacao
        {
            var query = _context.Usuarios.AsQueryable();

            query = query.Where(u => u.Nome != usuarioParametro.UsuarioAtual);
            query = query.Where(u => u.Genero == usuarioParametro.Genero);//vai retonar o genero oposto

            var minIdade = DateOnly.FromDateTime(DateTime.Today.AddYears(-usuarioParametro.MaxIdade - 1)); //usuario filtrar por idade
            var maxIdade = DateOnly.FromDateTime(DateTime.Today.AddYears(-usuarioParametro.MinIdade));

            query = query.Where(u => u.DataNascimento >= minIdade && u.DataNascimento <= maxIdade);

            query = usuarioParametro.OrderBy switch
            {
                "criado" => query.OrderByDescending(u => u.Criado),
                _ => query.OrderByDescending(u => u.UltimaVezAtivo)
            };

            return await PaginaLista<MembroDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<MembroDto>(_mapper.ConfigurationProvider), 
                usuarioParametro.NumeroPagina, 
                usuarioParametro.TamanhoPagina);
        }

        public async Task<IEnumerable<AppUser>> GetUserAsync()
        {
            return await _context.Usuarios
            .Include(f => f.Fotos)
            .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string nome)
        {
            return await _context.Usuarios
            .Include(f => f.Fotos)
            .SingleOrDefaultAsync(x => x.Nome == nome);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
        }
    }
}