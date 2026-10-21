import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const roleGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const expectedRoles: string[] = route.data?.['roles'] || [];
  const currentUser = authService.currentUser();

  // Si no hay usuario autenticado, redirigir a login
  if (!currentUser) {
    return router.createUrlTree(['/login']);
  }

  // HU-003: Validar si el rol del usuario cuenta con permiso para esta ruta
  if (expectedRoles.length === 0 || expectedRoles.includes(currentUser.rol)) {
    return true;
  }

  // HU-003 Escenario 2 y 3: Acceso denegado por rol -> redirige a una vista autorizada
  // Si es Solicitante lo enviamos a solicitudes; a los demás al dashboard
  if (currentUser.rol === 'Solicitante') {
    return router.createUrlTree(['/solicitudes']);
  }

  return router.createUrlTree(['/dashboard']);
};