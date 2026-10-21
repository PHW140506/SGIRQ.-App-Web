import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  identificador = 'jrodriguez';
  password = 'Admin123*';
  recordarme = true;
  showPassword = false;

  // Estado del modal "Datos incorrectos" (Página 2 del PDF)
  showErrorModal = false;
  modalTitle = 'Datos incorrectos';
  modalMessage = 'El usuario o la contraseña no son válidos.\nVerifica tus datos e inténtalo nuevamente.';

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    // HU-001 Escenario 3: Campos vacíos
    if (!this.identificador.trim() || !this.password.trim()) {
      this.modalTitle = 'Campos incompletos';
      this.modalMessage = 'Todos los campos son obligatorios. Por favor ingresa tu usuario y contraseña.';
      this.showErrorModal = true;
      return;
    }

    this.authService.login(this.identificador.trim(), this.password.trim()).subscribe({
      next: () => {
        // HU-001 Escenario 1: Acceso válido al panel
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        // HU-001 Escenario 2 / HU-007 Escenario 2
        this.modalTitle = 'Datos incorrectos';
        this.modalMessage = err.error?.mensaje || 'El usuario o la contraseña no son válidos. Verifica tus datos e inténtalo nuevamente.';
        this.showErrorModal = true;
      }
    });
  }

  closeModal(): void {
    this.showErrorModal = false;
  }
}