import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent{
  @Output() cancelarRegistro = new EventEmitter(); //emite um evento dentro do registro component
  registroForm!: FormGroup;
  maxDate: Date = new Date();
  validationErrors: string[] | undefined;

  constructor(private accountService: AccountService, private toastr: ToastrService,
    private fb: FormBuilder, private router: Router) { } //reacte forms, o router retorna para a pagina de membros depois que o usuario se cadastra

  ngOnInit(): void{
    this.InicializarForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18); //verfifca se é menor de idade
  }

  InicializarForm() { //reactive form
    this.registroForm = this.fb.group({
      genero: ['Masculino'], //pode adicionar validadores
      nomeUsuario: ['', Validators.required],
      conhecidoComo: ['', Validators.required],
      dataNascimento: ['', Validators.required],
      cidade: ['', Validators.required],
      pais: ['', Validators.required],
      senha: ['', [Validators.required,
      Validators.maxLength(8), Validators.minLength(4)]],
      confirmarSenha: ['', [Validators.required, this.ValoresIguais('senha')]]
    });
    this.registroForm.controls['senha'].valueChanges.subscribe({
      next: () => this.registroForm.controls['confirmarSenha'].updateValueAndValidity()
    })
  }

  ValoresIguais(senhasIguais: string): ValidatorFn{ //senha e confirmação de senha iguais
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(senhasIguais)?.value ? null : {senhasDiferentes: true}
    }
  }

  registro() {
    const dataNas = this.getDateOnly(this.registroForm.controls['dataNascimento'].value);
    const values = { ...this.registroForm.value, dataNascimento: dataNas };
    this.accountService.registro(values).subscribe({ //o que vamos fazer com a resposta
      next: () => {
        this.router.navigateByUrl('/membros'); //apos registrar vai a pagina de membros
      },
      error: error => {
        this.validationErrors = error;
      }
    })
  }
  //emite o que estamos produzindo a partir do compoenet filho
  cancelar() {
    this.cancelarRegistro.emit(false);
  }

  private getDateOnly(dataNas: string | undefined){
    if (!dataNas) return; //verifica se a data de nascimento esta vazia
    let data = new Date(dataNas);
    new Date(data.setMinutes(data.getMinutes() - data.getTimezoneOffset())).toString().slice(0, 10) //obtem os caracteres de 0 a 10
    const dataFormatada = data.toLocaleDateString('pt-BR'); 
    return dataFormatada;
  }
  
  
}
