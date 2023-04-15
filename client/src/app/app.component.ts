import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{ // injeção
  title = 'Dating App';
  usuarios: any; //tipo qualquer

  constructor(private http: HttpClient) { }
  
  ngOnInit(): void { // pedido ao servidor API
    this.http.get('http://localhost:5001/api/usuarios').subscribe({
      //tras os dados da api
      next: response => this.usuarios = response, //responde uma lista de usuarios
      error: error => console.log(error),
      complete: () => console.log('requisição concluida')

    })

    //fluxo de dados observavei que queremos observar
    //lambda adiciona o q recebemos de volta da resposta caso ocorra um erro
      //quando estiver concluido
     //obtem dados da API
  }

    
}
