namespace API.Helpers
{
    public class CabecalhoPaginacao
    {
        public CabecalhoPaginacao(int paginaAtual, int itensPorPagina, int totalItens, int totalPaginas)
        {
            PaginaAtual = paginaAtual;
            ItensPorPagina = itensPorPagina;
            TotalItens = totalItens;
            TotalPaginas = totalPaginas;
        }

        public int PaginaAtual { get; set; }
        public int ItensPorPagina { get; set; }
        public int TotalItens { get; set; }
        public int TotalPaginas { get; set; }
    }
}