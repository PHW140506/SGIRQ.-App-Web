import { Injectable, signal } from '@angular/core';
import { ApiService } from '../../core/api.service';
import { Reactivo, TrazabilidadItem } from '../../core/models';
import { errorText } from '../../core/errors';
@Injectable()
export class TrazabilidadViewModel {
  reactivos = signal<Reactivo[]>([]);
  movimientos = signal<TrazabilidadItem[]>([]);
  error = signal('');
  constructor(private api: ApiService) {}
  async cargar() {
    try { this.reactivos.set(await this.api.reactivos()); this.error.set(''); }
    catch(e) { this.error.set(errorText(e)); }
  }
  async consultar(id: number) {
    if (id <= 0) return;
    try { this.movimientos.set(await this.api.trazabilidad(id)); this.error.set(''); }
    catch(e) { this.error.set(errorText(e)); }
  }
}
