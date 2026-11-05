import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginViewModel } from './login.viewmodel';
@Component({ selector: 'app-login', standalone: true, imports: [FormsModule],
  providers: [LoginViewModel], templateUrl: './login.component.html' })
export class LoginComponent {
  usuario = ''; contrasenia = ''; mostrar = false;
  constructor(public vm: LoginViewModel) {}
}
