import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; //quando adiciona o import la em baixo tem q por aqui em cima

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { RegistroComponent } from './registro/registro.component';
import { ListaMembrosComponent } from './membros/lista-membros/lista-membros.component';
import { DetalhesMembrosComponent } from './membros/detalhes-membros/detalhes-membros.component';
import { ListasComponent } from './listas/listas.component';
import { MensagensComponent } from './mensagens/mensagens.component';
import { SharedModule } from './_modules/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegistroComponent,
    ListaMembrosComponent,
    DetalhesMembrosComponent,
    ListasComponent,
    MensagensComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    //requisição http HttpClientModule requisição de formulario FormsModule
    FormsModule,
    BrowserAnimationsModule, //usar os formulários
    SharedModule //importações por exemplo de tema
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
