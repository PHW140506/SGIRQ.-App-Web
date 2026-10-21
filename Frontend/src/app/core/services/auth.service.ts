import { Injectable, signal, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

export interface AuthResponse {
  token: string;
  usuario: string;
  nombreCompleto: string;
  rol: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5051/api/auth';

  readonly currentUser = signal<AuthResponse | null>(this.getStoredUser());

  login(identificador: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, { identificador, password }).pipe(
      tap(res => {
        localStorage.setItem('sgirq_token', res.token);
        localStorage.setItem('sgirq_user', JSON.stringify(res));
        this.currentUser.set(res);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('sgirq_token');
    localStorage.removeItem('sgirq_user');
    this.currentUser.set(null);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('sgirq_token');
  }

  private getStoredUser(): AuthResponse | null {
    const raw = localStorage.getItem('sgirq_user');
    return raw ? JSON.parse(raw) : null;
  }
  recuperarAcceso(correo: string): Observable<{ mensaje: string }> {
    return this.http.post<{ mensaje: string }>(`${this.apiUrl}/recuperar-acceso`, { correo });
  }
}