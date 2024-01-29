import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Membro } from 'src/app/_models/membro';
import { Usuario } from 'src/app/_models/usuario';
import { AccountService } from 'src/app/_services/account.service';
import { MembrosService } from 'src/app/_services/membros.service';

@Component({
  selector: 'app-editar-membro',
  templateUrl: './editar-membro.component.html',
  styleUrls: ['./editar-membro.component.css']
})
export class EditarMembroComponent implements OnInit{
  @ViewChild('editForm') editForm: NgForm | undefined;
  @HostListener('window: beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm?.dirty) { //caso o usuario saia da pagina sem salvar as alterações
      $event.returnValue = true;
    }
  }
  membro: Membro | undefined;
  usuario: Usuario | null = null;

  constructor(private accountService: AccountService, private membrosService: MembrosService,
    private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: usuario => this.usuario = usuario
    })
  }

  ngOnInit(): void{
    this.loadMembro();
  }

  loadMembro() {
    if (!this.usuario) return;
    this.membrosService.getMembro(this.usuario.nomeUsuario).subscribe({
      next: membro => this.membro = membro
    })
  }

  updateMembro() {
    this.membrosService.updateMembro(this.editForm?.value).subscribe({
      next: _ => {
        this.toastr.success('Perfil atualizado com sucesso');
        this.editForm?.reset(this.membro); //acessar img e outras informações
      }
    })
    
  }

}
