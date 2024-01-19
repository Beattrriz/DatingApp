import { Foto } from "./foto";

export interface Membro{
    id: number;
    nome: string;
    fotoUrl: string;
    idade: number;
    conhecidoComo: string;
    criado: Date;
    ultimaVezAtivo: Date;
    genero: string;
    introducao: string;
    procurandoPor: string;
    interesses: string;
    cidade: string;
    pais: string;
    fotos: Foto[];
}