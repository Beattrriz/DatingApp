using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikesRepository : ILikeRepository
    {
        private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
        {
            return await _context.Likes.FindAsync(sourceUserId, targetUserId);
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId)
        {
            var usuarios = _context.Usuarios.OrderBy(u => u.Nome).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if(predicate == "liked")
            {
                likes = likes.Where(like => like.SourceUserId == userId);
                usuarios = likes.Select(like => like.TargetUser);
            }

            if(predicate == "likedBy")
            {
                likes = likes.Where(like => like.TargetUserId == userId);
                usuarios = likes.Select(like => like.SourceUser);
            }

            return await usuarios.Select(usuario => new LikeDto
            {
                NomeUsuario = usuario.Nome,
                ConhecidoComo = usuario.ConhecidoComo,
                Idade = usuario.DataNascimento.CalcularIdade(),
                FotoUrl = usuario.Fotos.FirstOrDefault(x => x.Principal).Url,
                Cidade = usuario.Cidade,
                Id = usuario.Id
            }).ToListAsync();
        }

        public async Task<AppUser> GetUserWithLike(int userId)
        {
           return await _context.Usuarios
           .Include(x => x.LikedUsers)
           .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}