import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { Membro } from 'src/app/_models/membro';
import { MembrosService } from 'src/app/_services/membros.service';

@Component({
  selector: 'app-detalhes-membros',
  standalone: true,
  templateUrl: './detalhes-membros.component.html',
  styleUrls: ['./detalhes-membros.component.css'],
  imports: [CommonModule, TabsModule, GalleryModule] //removeu erros de detalhes-membros
})
export class DetalhesMembrosComponent implements OnInit{
  membro: Membro | undefined;
  imagens: GalleryItem[] = [];

  constructor(private memberService: MembrosService, private route: ActivatedRoute) {
    
  }

  ngOnInit(): void{
    this.loadMember();
  }

  loadMember() {
    const nomeUsuario = this.route.snapshot.paramMap.get('nome');
    if(!nomeUsuario) return
    this.memberService.getMembro(nomeUsuario).subscribe({
      next: membro => {
        this.membro = membro,
        this.getImagens()
      }
    })
  }

  getImagens() {
    if (!this.membro) return;
    for (const foto of this.membro?.fotos) {
      this.imagens.push(new ImageItem({ src: foto.url, thumb: foto.url }));
    }
  }
}
