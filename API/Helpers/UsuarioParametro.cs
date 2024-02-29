using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class UsuarioParametro
    {
        private const int TamMaxDaPagina = 50;

        public int NumeroPagina { get; set; } = 1;

        private int _tamanhoPagina = 10; //qual tamanho quer retornar

        public int TamanhoPagina
        {
            get => _tamanhoPagina;
            set => _tamanhoPagina = (value > TamMaxDaPagina) ? TamMaxDaPagina : value; //verifica se o valor passa de 50 se passar retorna 50
        }
        
        public string UsuarioAtual { get; set; }
        public string Genero { get; set; }
        public int MinIdade { get; set; } = 18;
        public int MaxIdade { get; set; } = 100;
        public string OrderBy { get; set; } = "ultimaVezAtivo";

    }
}