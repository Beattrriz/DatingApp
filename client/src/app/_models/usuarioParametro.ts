import { Usuario } from "./usuario";

export class UsuarioParametro{
    genero: string;
    minIdade = 18;
    maxIdade = 99;
    numeroPagina = 1;
    tamanhoPagina = 5;
    orderBy = 'ultimaVezAtivo';

    constructor(usuario: Usuario) {
        this.genero = usuario.genero === 'Feminino' ? 'Masculino' : 'Feminino'
    }
}