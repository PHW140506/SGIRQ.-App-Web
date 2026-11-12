import { Injectable, signal } from '@angular/core';
import { ApiService } from '../../core/api.service';
import { Salida } from '../../core/models';
import { errorText } from '../../core/errors';
@Injectable()
export class SolicitudesViewModel {
  usuarios = signal<number[]>([]);
  salidas = signal<Salida[]>([]);
  error = signal('');
  constructor(private api: ApiService) {}
  async cargar() {
    try { this.usuarios.set(await this.api.solicitantes()); this.error.set(''); }
    catch (e) { this.error.set(errorText(e)); }
  }
  async consultar(id: number) {
    if (id <= 0) return;
    try { this.salidas.set(await this.api.solicitudes(id)); this.error.set(''); }
    catch (e) { this.error.set(errorText(e)); }
  }
}
