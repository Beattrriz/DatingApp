import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http'; //quando adiciona o import la em baixo tem q por aqui em cima

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { RegistroComponent } from './registro/registro.component';
import { ListaMembrosComponent } from './membros/lista-membros/lista-membros.component';
import { DetalhesMembrosComponent } from './membros/detalhes-membros/detalhes-membros.component';
import { ListasComponent } from './listas/listas.component';
import { MensagensComponent } from './mensagens/mensagens.component';
import { SharedModule } from './_modules/shared.module';
import { TestErroComponent } from './erros/test-erro/test-erro.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './erros/not-found/not-found.component';
import { ServerErrorComponent } from './erros/server-error/server-error.component';
import { CardMembroComponent } from './membros/card-membro/card-membro.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { EditarMembroComponent } from './membros/editar-membro/editar-membro.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { FotoEditorComponent } from './membros/foto-editor/foto-editor.component';
import { MensagensFormComponent } from './_forms/mensagens-form/mensagens-form.component';
import { DatePickerComponent } from './_forms/date-picker/date-picker.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegistroComponent,
    ListaMembrosComponent,
    ListasComponent,
    MensagensComponent,
    TestErroComponent,
    NotFoundComponent,
    ServerErrorComponent,
    CardMembroComponent,
    EditarMembroComponent,
    FotoEditorComponent,
    MensagensFormComponent,
    DatePickerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, //rotas
    HttpClientModule,
    //requisição http HttpClientModule requisição de formulario FormsModule
    FormsModule,
    ReactiveFormsModule, //form reativo
    BrowserAnimationsModule, //usar os formulários
    SharedModule //importações por exemplo de tema
  ],
  providers: [
    //tem que passar o interceptor que criamos ng g inteceptor _interceptor/error
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
