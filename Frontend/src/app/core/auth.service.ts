import { Injectable, signal } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from './api.service';
import { Session } from './models';
@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly storageKey = 'sgirq-session';
  readonly session = signal<Session | null>(this.read());
  constructor(private api: ApiService, private router: Router) {}
  private read(): Session | null {
    try { return JSON.parse(sessionStorage.getItem(this.storageKey) || 'null'); }
    catch { return null; }
  }
  async login(usuario: string, password: string) {
    const session = await this.api.login(usuario, password);
    sessionStorage.setItem(this.storageKey, JSON.stringify(session));
    this.session.set(session);
    await this.router.navigateByUrl(session.rol === 'Solicitante' ? '/cuenta' : '/dashboard');
  }
  logout() {
    sessionStorage.removeItem(this.storageKey);
    this.session.set(null);
    void this.router.navigateByUrl('/login');
  }
  isAdmin() { return this.session()?.rol === 'Administrador'; }
}
