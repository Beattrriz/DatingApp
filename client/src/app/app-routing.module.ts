import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListaMembrosComponent } from './membros/lista-membros/lista-membros.component';
import { DetalhesMembrosComponent } from './membros/detalhes-membros/detalhes-membros.component';
import { ListasComponent } from './listas/listas.component';
import { MensagensComponent } from './mensagens/mensagens.component';
import { autenticacaoGuard } from './_guards/autenticacao.guard';
import { TestErroComponent } from './erros/test-erro/test-erro.component';
import { NotFoundComponent } from './erros/not-found/not-found.component';
import { ServerErrorComponent } from './erros/server-error/server-error.component';
import { EditarMembroComponent } from './membros/editar-membro/editar-membro.component';
import { evitarAlteracoesNaoSalvasGuard } from './_guards/evitar-alteracoes-nao-salvas.guard';

const routes: Routes = [
  //cada raiz é um objeto
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [autenticacaoGuard],
    children: [ //verifica se o usuario pode acessar essa rota
      {
        path: 'membros', component: ListaMembrosComponent}, 
      { path: 'membros/:nome', component: DetalhesMembrosComponent },
      { path: 'membro/editar', component: EditarMembroComponent, canDeactivate: [evitarAlteracoesNaoSalvasGuard] },
      { path: 'listas', component: ListasComponent },
      { path: 'mensagens', component: MensagensComponent },
    ]
  },
  { path: 'errors', component: TestErroComponent },
  { path: 'not-found', component: NotFoundComponent },
  {path: 'server-error', component: ServerErrorComponent},
  { path: '**', component: NotFoundComponent, pathMatch: 'full' }, //qualquer coisa que não esteja nesta lista
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
