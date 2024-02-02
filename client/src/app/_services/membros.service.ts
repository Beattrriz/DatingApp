import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Membro } from '../_models/membro';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembrosService {
  baseUrl = environment.apiUrl;
  membros: Membro[] = [];

  constructor(private http: HttpClient) { }

  getMembros() {
    if (this.membros.length > 0) return of(this.membros); //armazenar os membros no serviço sem precisar procurar na API
    return this.http.get<Membro[]>(this.baseUrl + 'usuarios').pipe(
      map(membros => {
        this.membros = membros;
        return membros;
      })
    )
  }

  getMembro(nome: string)
  {
    const membro = this.membros.find(x => x.nome == nome);
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
 
}
