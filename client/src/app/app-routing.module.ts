import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListaMembrosComponent } from './membros/lista-membros/lista-membros.component';
import { DetalhesMembrosComponent } from './membros/detalhes-membros/detalhes-membros.component';
import { ListasComponent } from './listas/listas.component';
import { MensagensComponent } from './mensagens/mensagens.component';
import { autenticacaoGuard } from './_guards/autenticacao.guard';

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
      { path: 'membros/:id', component: DetalhesMembrosComponent },
      { path: 'listas', component: ListasComponent },
      { path: 'mensagens', component: MensagensComponent },
    ]
  },
  { path: '**', component: HomeComponent, pathMatch: 'full' }, //qualquer coisa que não esteja nesta lista
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
