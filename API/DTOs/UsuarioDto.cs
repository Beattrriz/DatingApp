using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UsuarioDto
    {
        public string NomeUsuario { get; set; }

        public string Token { get; set; }

        public string FotoUrl { get; set; }

        public string ConhecidoComo { get; set; }

        public string Genero { get; set; }
    }
}