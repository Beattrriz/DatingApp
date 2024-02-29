using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class PaginaLista<T> : List<T>//t tipo generico
    {
        public PaginaLista(IEnumerable<T> items, int quantidadePaginas, int numeroPagina, int tamanhoPagina)
        {
            PaginaAtual = numeroPagina;
            TotalPaginas = (int)Math.Ceiling(Count / (double)tamanhoPagina);
            TamanhoPagina = tamanhoPagina;
            ContagemTotal = quantidadePaginas;
            AddRange(items);
        }

        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int ContagemTotal { get; set; }

        public static async Task<PaginaLista<T>> CreateAsync(IQueryable<T> source,
        int numeroPagina, int tamanhoPagina){
            var quantidadePaginas = await source.CountAsync();
            var items = await source.Skip((numeroPagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();
            return new PaginaLista<T>(items, quantidadePaginas, numeroPagina, tamanhoPagina);
        }
    }
}