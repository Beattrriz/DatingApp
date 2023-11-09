import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent{
  
  registerMode = false;
  usuarios: any;

  constructor(private http: HttpClient) { }
  
  ngOnInit(): void{
    this.getUsers();
  }
   //exibir ou não a home page
  AlternarRegistro() {
    this.registerMode = !this.registerMode;
  }
   
  getUsers() {
    this.http.get('http://localhost:5001/api/usuarios').subscribe({
      //tras os dados da api
      next: response => this.usuarios = response, //responde uma lista de usuarios
      error: error => console.log(error),
      complete: () => console.log('requisição concluida')
     //obtem dados da API
    })
  }

  cancelarModoRegistro(event: boolean) {
    this.registerMode = event;
  }
}
