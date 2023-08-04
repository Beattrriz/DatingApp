using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegistroDto
    {
        [Required]

        public string NomeUsuario { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}