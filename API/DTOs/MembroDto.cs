
namespace API.DTOs
{
    public class MembroDto
    { //automaper ajudar no mapeamento de uma entidade para um  DTO
        public int Id { get; set; }
        public string Nome { get; set; }
        public string FotoUrl { get; set; }
        public int Idade { get; set; }
        public string ConhecidoComo { get; set; }
        public DateTime Criado { get; set; } 
        public DateTime UltimaVezAtivo { get; set; }
        public string Genero { get; set; }
        public string Introducao { get; set; }
        public string ProcurandoPor { get; set; }
        public string Interesses { get; set; }
        public string Cidade { get; set; }
        public string Pais { get; set; }
        public List<FotoDto> Fotos { get; set; } 

    }

}