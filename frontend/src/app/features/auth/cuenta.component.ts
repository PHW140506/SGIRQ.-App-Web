import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../core/api.service';
import { errorText } from '../../core/errors';
@Component({ selector: 'app-cuenta', standalone: true, imports: [FormsModule],
  template: `<h1>Mi cuenta</h1><section class="panel narrow"><h2>Cambiar contraseña</h2>
  <form (ngSubmit)="guardar()"><label>Contraseña actual<input type="password" name="actual" [(ngModel)]="actual" required></label>
  <label>Nueva contraseña (mínimo 10 caracteres)<input type="password" name="nueva" [(ngModel)]="nueva" required minlength="10"></label>
  <button class="primary">Actualizar contraseña</button></form>
  @if (mensaje()) { <p role="status">{{ mensaje() }}</p> }</section>` })
export class CuentaComponent {
  actual = ''; nueva = ''; mensaje = signal('');
  constructor(private api: ApiService) {}
  async guardar() {
    try { await this.api.cambiarContrasenia(this.actual, this.nueva);
      this.actual = ''; this.nueva = ''; this.mensaje.set('Contraseña actualizada.'); }
    catch(e) { this.mensaje.set(errorText(e)); }
  }
}
