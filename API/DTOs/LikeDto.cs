namespace API.DTOs
{
    public class LikeDto
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; }

        public int Idade { get; set; }
        public string ConhecidoComo { get; set; }
        public string Cidade { get; set; }
        public string FotoUrl { get; set; }
    }
}