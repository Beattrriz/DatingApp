export interface Paginacao{
    paginaAtual: number;
    itensPorPagina: number;
    totalItens: number;
    totalPaginas: number;
}

export class ResultadoPaginacao<T>{
    result?: T //novos resultados paginados
    pagination?: Paginacao;
}