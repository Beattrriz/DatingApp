import { Component } from '@angular/core';
import { Membro } from 'src/app/_models/membro';
import { MembrosService } from 'src/app/_services/membros.service';

@Component({
  selector: 'app-lista-membros',
  templateUrl: './lista-membros.component.html',
  styleUrls: ['./lista-membros.component.css']
})
export class ListaMembrosComponent {
  membros: Membro[] = [];

  constructor(private membroService: MembrosService) { }

  ngOnInit(): void {
    this.loadMembros();
  }
  
  loadMembros() {
    this.membroService.getMembros().subscribe({
      next: membros => this.membros = membros
    })
  }

}
