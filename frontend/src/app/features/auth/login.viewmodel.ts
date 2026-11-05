import { Injectable, signal } from '@angular/core';
import { AuthService } from '../../core/auth.service';
import { errorText } from '../../core/errors';
@Injectable()
export class LoginViewModel {
  readonly loading = signal(false);
  readonly error = signal('');
  constructor(private auth: AuthService) {}
  async ingresar(usuario: string, contrasenia: string) {
    this.error.set('');
    if (!usuario.trim() || !contrasenia.trim()) { this.error.set('Completa ambos campos.'); return; }
    this.loading.set(true);
    try { await this.auth.login(usuario.trim(), contrasenia); }
    catch (error) { this.error.set(errorText(error)); }
    finally { this.loading.set(false); }
  }
}
