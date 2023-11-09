import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; //quando adiciona o import la em baixo tem q por aqui em cima

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { RegistroComponent } from './registro/registro.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegistroComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    //requisição http HttpClientModule requisição de formulario FormsModule
    FormsModule,
    //dropdownlist do ngx bootstrap
    BsDropdownModule.forRoot(),
    BrowserAnimationsModule //usar os formulários
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
