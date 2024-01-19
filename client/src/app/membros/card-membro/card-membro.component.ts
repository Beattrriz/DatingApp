import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { Membro } from 'src/app/_models/membro';
//cartões de membros serão filhos do componente de lista de membros
@Component({
  selector: 'app-card-membro',
  templateUrl: './card-membro.component.html',
  styleUrls: ['./card-membro.component.css']
})
export class CardMembroComponent implements OnInit{
  @Input() membro: Membro | undefined;

  constructor() {
    
  }

  ngOnInit(): void {
    
  }
}
