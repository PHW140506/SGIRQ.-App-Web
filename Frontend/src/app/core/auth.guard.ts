import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';
export const authGuard: CanActivateFn = () => {
  const auth = inject(AuthService);
  return auth.session() ? true : inject(Router).parseUrl('/login');
};
export const adminGuard: CanActivateFn = () =>
  inject(AuthService).isAdmin() ? true : inject(Router).parseUrl('/dashboard');
export const staffGuard: CanActivateFn = () =>
  ['Administrador','Auxiliar'].includes(inject(AuthService).session()?.rol ?? '')
    ? true : inject(Router).parseUrl('/login');
