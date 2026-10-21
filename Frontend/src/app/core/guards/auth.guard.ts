import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  // HU-002 Escenario 2: Intento abrir página protegida después de salir y se pide iniciar sesión
  if (authService.isAuthenticated()) {
    return true;
  }

  return router.createUrlTree(['/login']);
};