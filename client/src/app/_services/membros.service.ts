import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Membro } from '../_models/membro';

@Injectable({
  providedIn: 'root'
})
export class MembrosService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMembros() {
    return this.http.get<Membro[]>(this.baseUrl + 'usuarios');
  }

  getMembro(nome: string)
  {
    return this.http.get<Membro>(this.baseUrl + 'usuarios/' + nome);
  }
 
}
