using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MembroUpdateDto
    {
        public string Introducao { get; set; }
        public string ProcurandoPor { get; set; }
        public string Interesses { get; set; }
        public string Cidade { get; set; }
        public string Pais { get; set; }
    }
}