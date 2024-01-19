import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent{
  
  registerMode = false;
  usuarios: any;

  constructor() { }
  
  ngOnInit(): void{
  }
   //exibir ou não a home page
  AlternarRegistro() {
    this.registerMode = !this.registerMode;
  }
   
  cancelarModoRegistro(event: boolean) {
    this.registerMode = event;
  }
}
