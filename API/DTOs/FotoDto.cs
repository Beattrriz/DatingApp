using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class FotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool Principal { get; set; }
    }
}