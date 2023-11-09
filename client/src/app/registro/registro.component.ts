import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent{
  @Output() cancelarRegistro = new EventEmitter(); //emite um evento dentro do registro component
  model: any = {}

  constructor(private accountService: AccountService) {}

  NgOnInit(): void{
  }

  registro() {
    this.accountService.registro(this.model).subscribe({ //o que vamos fazer com a resposta
      next: () => {
        this.cancelar(); //apos registrar volta a pagina home
      },
      error: error => console.log(error)
    })
  }
  //emite o que estamos produzindo a partir do compoenet filho
  cancelar() {
    this.cancelarRegistro.emit(false);
  }
}
