using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string ConhecidoComo { get; set; }
        public DateTime Criado { get; set; } = DateTime.UtcNow;
        public DateTime UltimaVezAtivo { get; set; } = DateTime.UtcNow;
        public string Genero { get; set; }
        public string Introducao { get; set; }
        public string ProcurandoPor { get; set; }
        public string Interesses { get; set; }
        public string Cidade { get; set; }
        public string Pais { get; set; }
        public List<Foto> Fotos { get; set; } = new();

        public List<UserLike> LikedByUsers {get; set;}
         public List<UserLike> LikedUsers {get; set;}

        /*public int GetIdade()
        {
            return DataNascimento.CalcularIdade();
        }*/
    }

}