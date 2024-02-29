import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Membro } from '../_models/membro';
import { map, of, take } from 'rxjs';
import { ResultadoPaginacao } from '../_models/paginacao';
import { UsuarioParametro } from '../_models/usuarioParametro';
import { AccountService } from './account.service';
import { Usuario } from '../_models/usuario';

@Injectable({
  providedIn: 'root'
})
export class MembrosService {
  baseUrl = environment.apiUrl;
  membros: Membro[] = [];
  membroCache = new Map();
  usuario: Usuario | undefined;
  usuarioParametro: UsuarioParametro | undefined;

  constructor(private http: HttpClient, private accountService: AccountService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: usuario => {
      if(usuario) {
        this.usuarioParametro = new UsuarioParametro(usuario);
        this.usuario = usuario;
        }
      }
    })
  }

  getUsuarioParametro() {
    return this.usuarioParametro
  }

  setUsuarioParametro(params: UsuarioParametro) {
    this.usuarioParametro = params;
  }

  resetarUsuarioParametro(){
    if (this.usuario) {
    this.usuarioParametro = new UsuarioParametro(this.usuario);
    return this.usuarioParametro
    }
    return;
  }

  getMembros(usuarioParametro: UsuarioParametro) {//enviar como pergunta
    const response = this.membroCache.get(Object.values(usuarioParametro).join('-'));

    if (response) return of(response);

    let params = this.getCabecalhoPaginacao(usuarioParametro.numeroPagina, usuarioParametro.tamanhoPagina);
    
    params = params.append('minIdade', usuarioParametro.minIdade);
    params = params.append('maxIdade', usuarioParametro.maxIdade);
    params = params.append('genero', usuarioParametro.genero);
    params = params.append('orderBy', usuarioParametro.orderBy);

    return this.getResultadoPaginacao<Membro[]>(this.baseUrl + 'usuarios', params).pipe(//carrega da memoria
      map(response => {
        this.membroCache.set(Object.values(usuarioParametro).join('-'), response);
        return response;
      })
    )
  }

  getMembro(nome: string)
  {
    const membro = [...this.membroCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])//matriz
      .find((membro: Membro) => membro.nome === nome);

    if (membro) return of(membro);

    return this.http.get<Membro>(this.baseUrl + 'usuarios/' + nome);
  }

  updateMembro(membro: Membro) {
    return this.http.put(this.baseUrl + 'usuarios', membro).pipe(
      map(() => {
        const index = this.membros.indexOf(membro);
        this.membros[index] = {...this.membros[index], ...membro} //atualzia o membro com as informações atualizadas
      })
    )
  }

  SetFotoPrincipal(fotoId: number) {
    return this.http.put(this.baseUrl + 'usuarios/definir-foto-principal/' + fotoId, {});
  }

  DeletarFoto(fotoId: number) {
    return this.http.delete(this.baseUrl + 'usuarios/deletar-foto/' + fotoId);
  }

  private getResultadoPaginacao<T>(url: string, params: HttpParams) {
    const resultadoPaginacao: ResultadoPaginacao<T> = new ResultadoPaginacao<T>;
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        if (response.body) {
          resultadoPaginacao.result = response.body;
        }

        const pagination = response.headers.get('Pagination');
        if (pagination) {
          resultadoPaginacao.pagination = JSON.parse(pagination);
        }
        return resultadoPaginacao;
      })
    );
  }

  private getCabecalhoPaginacao(numeroPagina: number, tamanhoPagina: number) {
    let params = new HttpParams();

    params = params.append('NumeroPagina', numeroPagina);
    params = params.append('tamanhoPagina', tamanhoPagina);

    return params;
  }
 
}
