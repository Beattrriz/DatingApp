import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { Usuario } from './_models/usuario';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{ // injeção
  title = 'Dating App';
  usuarios: any; //tipo qualquer

    //serviço de conta privada
  constructor(private accountService: AccountService) { }
  
  ngOnInit(): void { // pedido ao servidor API
    this.setCurrentUser();
  }

  setCurrentUser() {
    const usuarioString = localStorage.getItem('usuario');
    if (!usuarioString) return;
    const usuario: Usuario = JSON.parse(usuarioString);
    this.accountService.setCurrentUser(usuario);
  }

    
}
