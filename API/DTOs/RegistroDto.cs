using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{//automapper
    public class RegistroDto
    {
        [Required] 
        public string NomeUsuario { get; set; }
        
        [Required] 
        public string ConhecidoComo { get; set; }
        
        [Required] 
        public string Genero { get; set; }
        
        [Required]  
        public DateOnly? DataNascimento { get; set; } //opcional 
        
        [Required] 
        public string Cidade { get; set; }
       
        [Required] 
        public string Pais { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)] //max e min do tamanho da senha
        public string Senha { get; set; }
    }
}