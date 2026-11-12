import { HttpErrorResponse } from '@angular/common/http';
export function errorText(error: unknown): string {
  if (error instanceof HttpErrorResponse) {
    if (error.status === 0) return 'No fue posible conectar con el backend. Comprueba que esté iniciado.';
    if (error.status === 401) return 'Tu sesión expiró o las credenciales son incorrectas.';
    if (error.status === 403) return 'No tienes permiso para realizar esta operación.';
    return error.error?.message || 'No fue posible completar la operación.';
  }
  return error instanceof Error ? error.message : 'Ha ocurrido un error.';
}
