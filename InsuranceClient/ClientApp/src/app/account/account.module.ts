import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginFormComponent } from './login-form/login-form.component';
import { HomeComponent } from './home/home.component';
import { SpinnerComponent } from './spinner/spinner.component';



@NgModule({
  declarations: [LoginFormComponent, HomeComponent, SpinnerComponent],
  imports: [
    CommonModule
  ]
})
export class AccountModule { }
