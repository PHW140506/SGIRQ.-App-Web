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

  // Bandera para validación visual inline de login
  submitted = false;

  // Estado del modal institucional "Datos incorrectos" (Página 2 del PDF)
  showErrorModal = false;
  modalTitle = 'Datos incorrectos';
  modalMessage = 'El usuario o la contraseña no son válidos.\nVerifica tus datos e inténtalo nuevamente.';

  // HU-004: Estado del modal de recuperación de acceso
  showRecoveryModal = false;
  recoveryEmail = '';
  recoverySubmitted = false;
  simulateExpiredLink = false;
  recoveryFeedbackMessage = '';
  recoveryFeedbackType: 'success' | 'error' = 'success';

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    this.submitted = true;

    // HU-001 Escenario 3
    if (!this.identificador.trim() || !this.password.trim()) {
      return;
    }

    this.authService.login(this.identificador.trim(), this.password.trim()).subscribe({
      next: () => {
        // HU-001 Escenario 1
        this.router.navigate(['/dashboard']);
      },
      error: (err: any) => {
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

  // HU-004: Abrir y cerrar modal de recuperación
  openRecovery(): void {
    this.recoveryEmail = '';
    this.recoverySubmitted = false;
    this.simulateExpiredLink = false;
    this.recoveryFeedbackMessage = '';
    this.showRecoveryModal = true;
  }

  closeRecovery(): void {
    this.showRecoveryModal = false;
  }

  // HU-004: Procesar solicitud de recuperación
  onSubmitRecovery(): void {
    this.recoverySubmitted = true;
    this.recoveryFeedbackMessage = '';

    if (!this.recoveryEmail.trim()) {
      return;
    }

    // HU-004 Escenario 2: Enlace vencido o token expirado
    if (this.simulateExpiredLink) {
      this.recoveryFeedbackType = 'error';
      this.recoveryFeedbackMessage = 'HU-004 (Esc. 2): El enlace de recuperación ha expirado. Se rechaza el restablecimiento por seguridad.';
      return;
    }

    // HU-004 Escenarios 1 y 3: Solicitud válida con Respuesta Neutral
    this.authService.recuperarAcceso(this.recoveryEmail.trim()).subscribe({
      next: (res) => {
        this.recoveryFeedbackType = 'success';
        this.recoveryFeedbackMessage = res.mensaje;
      },
      error: (err: any) => {
        this.recoveryFeedbackType = 'error';
        this.recoveryFeedbackMessage = err.error?.mensaje || 'Ocurrió un error al procesar la solicitud.';
      }
    });
  }
}