using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUsuarioRepository
    {
        void Update(AppUser usuario);

        Task<bool> SaveAllAsync();

        Task<IEnumerable<AppUser>> GetUserAsync();

        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByUsernameAsync(string nome);

        Task<IEnumerable<MembroDto>> GetMembersAsync();

        Task<MembroDto> GetMemberAsync(string nome);
    }
}