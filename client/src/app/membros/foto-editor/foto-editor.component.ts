import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Foto } from 'src/app/_models/foto';
import { Membro } from 'src/app/_models/membro';
import { Usuario } from 'src/app/_models/usuario';
import { AccountService } from 'src/app/_services/account.service';
import { MembrosService } from 'src/app/_services/membros.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-foto-editor',
  templateUrl: './foto-editor.component.html',
  styleUrls: ['./foto-editor.component.css']
})
export class FotoEditorComponent implements OnInit{
  @Input() membro: Membro | undefined;
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  usuario: Usuario | undefined;

  constructor(private accountService: AccountService, private membroService: MembrosService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: usuario => {
        if(usuario) this.usuario = usuario
      }
    })
  }

  ngOnInit(): void{
    this.iniciarUploader();
  }

  fileOverBase(e: any) { //evento de tipo qualquer
    this.hasBaseDropZoneOver = e;
  }

  //precisamos atualiza a url do usuario, atualziar a coleção que existe em membro e colocar nova foto no main
  DefinirFotoPrincipal(foto: Foto) {
    this.membroService.SetFotoPrincipal(foto.id).subscribe({
      next: () => {
        if (this.usuario && this.membro) {
          this.usuario.fotoUrl = foto.url;
          this.accountService.setCurrentUser(this.usuario);
          this.membro.fotoUrl = foto.url;
          this.membro.fotos.forEach(f => {
            if (f.principal) f.principal = false;//verifica se f é a foto principal
            if (f.id === foto.id) f.principal = true;
          })
        }
      }
    })
  }

  DeletarFoto(fotoId: number) {
    this.membroService.DeletarFoto(fotoId).subscribe({
      next: _ => {
        if (this.membro) {
          this.membro.fotos = this.membro.fotos.filter(x => x.id !== fotoId) //devolve todas as fotos menios a que eu quero excluir
        }
      }
    })
  }

  iniciarUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'usuarios/add-foto',
      authToken: 'Bearer ' + this.usuario?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true, //remover depois do upload
      autoUpload: false,//upload automatico nao
      maxFileSize: 10 * 1024 * 1024//max 10 megas
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false
    }

    this.uploader.onSuccessItem = (item, resposta, status, headers) => {
      if (resposta) {
        const foto = JSON.parse(resposta);
        this.membro?.fotos.push(foto);
      }
    } //sucesso>
  }

}
