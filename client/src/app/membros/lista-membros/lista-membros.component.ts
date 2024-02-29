import { Component } from '@angular/core';
import { Observable, take } from 'rxjs';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Membro } from 'src/app/_models/membro';
import { Paginacao } from 'src/app/_models/paginacao';
import { MembrosService } from 'src/app/_services/membros.service';
import { UsuarioParametro } from 'src/app/_models/usuarioParametro';
import { Usuario } from 'src/app/_models/usuario';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-lista-membros',
  templateUrl: './lista-membros.component.html',
  styleUrls: ['./lista-membros.component.css']
})
export class ListaMembrosComponent {
  //membros$: Observable<Membro[]> | undefined;
  membros: Membro[] = [];
  pagination: Paginacao | undefined;
  usuarioParametro: UsuarioParametro | undefined;
  generoLista = [{value: 'masculino', display: 'Masculino'}, {value: 'feminino', display: 'Feminino'}]

  constructor(private membroService: MembrosService) {
    this.usuarioParametro = this.membroService.getUsuarioParametro();
   }

  ngOnInit(): void {
    //this.membros$ = this.membroService.getMembros();
    this.loadMembros();
  }
  
  loadMembros() {
    if (this.usuarioParametro) {
      this.membroService.setUsuarioParametro(this.usuarioParametro);
      this.membroService.getMembros(this.usuarioParametro).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            this.membros = response.result;
            this.pagination = response.pagination;
          }
        }
      })
    } 
  }

  resetarFiltros() {
      this.usuarioParametro = this.membroService.resetarUsuarioParametro(); //volta para dados padrão
      this.loadMembros();
  }

  pageChanged(event: any) {
    if (this.usuarioParametro && this.usuarioParametro?.numeroPagina !== event.page) { //atualiza a propriedade numeroPagina do componente para ser igual ao número da página do evento.
      this.usuarioParametro.numeroPagina = event.page;
      this.membroService.setUsuarioParametro(this.usuarioParametro);
      this.loadMembros();
    }
  }
}
