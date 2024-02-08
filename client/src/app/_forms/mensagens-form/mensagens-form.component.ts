import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-mensagens-form',
  templateUrl: './mensagens-form.component.html',
  styleUrls: ['./mensagens-form.component.css']
})
export class MensagensFormComponent implements ControlValueAccessor{
  @Input() label = '';
  @Input() type = 'text';
  
  constructor(@Self() public ngControl: NgControl) { 
    this.ngControl.valueAccessor = this;
  } //decorator DOM
  
  writeValue(obj: any): void {
  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }

  get control(): FormControl{ //controle de form
    return this.ngControl.control as FormControl
  }


}
