using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Fotos")]
    public class Foto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool Principal { get; set; }
        public string IdPublico { get; set; }

        // relacionamento dotnet ef migrations add --nome
        // dotnet ef database update
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}