using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IUsuarioRepository
    {
        void Update(AppUser usuario);

        Task<bool> SaveAllAsync();

        Task<IEnumerable<AppUser>> GetUserAsync();

        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByUsernameAsync(string nome);

        //Task<IEnumerable<MembroDto>> GetMembersAsync();
        Task<PaginaLista<MembroDto>> GetMembersAsync(UsuarioParametro usuarioParametro);

        Task<MembroDto> GetMemberAsync(string nome);
    }
}