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

  identificador = '';
  password = '';
  recordarme = true;
  showPassword = false;

  // Bandera para activar la validación visual inline tras el primer intento de envío
  submitted = false;

  // Estado del modal institucional "Datos incorrectos" (Página 2 del PDF)
  showErrorModal = false;
  modalTitle = 'Datos incorrectos';
  modalMessage = 'El usuario o la contraseña no son válidos.\nVerifica tus datos e inténtalo nuevamente.';

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    this.submitted = true;

    // HU-001 Escenario 3: Si falta alguno, se marcan los campos en rojo con su texto y se detiene el envío
    if (!this.identificador.trim() || !this.password.trim()) {
      return;
    }

    this.authService.login(this.identificador.trim(), this.password.trim()).subscribe({
      next: () => {
        // HU-001 Escenario 1: Redirección al panel
        this.router.navigate(['/dashboard']);
      },
      error: (err: any) => {
        // HU-001 Escenario 2 / HU-007 Escenario 2: Despliegue del modal institucional
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