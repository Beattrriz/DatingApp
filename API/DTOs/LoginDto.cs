using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class LoginDto
    {
        public string NomeUsuario { get; set; }

        public string Senha { get; set; }
    }
}