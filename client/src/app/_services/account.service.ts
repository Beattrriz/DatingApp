import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Usuario } from '../_models/usuario';

@Injectable({
  providedIn: 'root'
})
export class AccountService { //requisições http ao client
  baseUrl = 'http://localhost:5001/api/';
  private currentUserSource = new BehaviorSubject<Usuario | null>(null); //observable
  currentUser$ = this.currentUserSource.asObservable(); //inobservable

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post<Usuario>(this.baseUrl + 'contas/login', model).pipe(
      map((response: Usuario) => {
        const usuario = response;
        if (usuario){
          localStorage.setItem('usuario', JSON.stringify(usuario))
          this.currentUserSource.next(usuario);
        }
      })
    )
  }

  //registro de usuarios
  registro(model: any) {
    return this.http.post<Usuario>(this.baseUrl + 'contas/registro', model).pipe(
      map(usuario => {
        if (usuario) {
          localStorage.setItem('usuario', JSON.stringify(usuario));
        }
      })
    )
  }

  setCurrentUser(usuario: Usuario){
    this.currentUserSource.next(usuario);
  }

  logout() {
    localStorage.removeItem('usuario');
    this.currentUserSource.next(null);
  }
}
