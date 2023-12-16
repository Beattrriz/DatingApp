using API.DTOs;
using API.Entities;
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

        public async Task<IEnumerable<MembroDto>> GetMembersAsync()
        {
            return await _context.Usuarios
            .ProjectTo<MembroDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
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