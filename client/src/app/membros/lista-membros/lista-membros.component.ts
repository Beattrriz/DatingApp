import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Membro } from 'src/app/_models/membro';
import { MembrosService } from 'src/app/_services/membros.service';

@Component({
  selector: 'app-lista-membros',
  templateUrl: './lista-membros.component.html',
  styleUrls: ['./lista-membros.component.css']
})
export class ListaMembrosComponent {
  membros$: Observable<Membro[]> | undefined;

  constructor(private membroService: MembrosService) { }

  ngOnInit(): void {
    this.membros$ = this.membroService.getMembros();
  }
  

}
