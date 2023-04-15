import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; //quando adiciona o import la em baixo tem q por aqui em cima

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
    //requisição http HttpClientModule requisição de formulario FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
