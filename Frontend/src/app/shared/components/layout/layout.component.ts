import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './layout.component.html'
})
export class LayoutComponent {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  readonly user = this.authService.currentUser;

  // Comprueba si el rol en sesión está dentro de los roles autorizados
  hasRole(allowedRoles: string[]): boolean {
    const rolActual = this.user()?.rol;
    return !!rolActual && allowedRoles.includes(rolActual);
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}