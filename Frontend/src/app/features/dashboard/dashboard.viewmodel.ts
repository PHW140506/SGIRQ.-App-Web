import { Injectable, signal } from '@angular/core';
import { ApiService } from '../../core/api.service';
import { Reactivo } from '../../core/models';
import { errorText } from '../../core/errors';
@Injectable()
export class DashboardViewModel {
  reactivos = signal<Reactivo[]>([]);
  error = signal('');
  constructor(private api: ApiService) {}
  async cargar() {
    try { this.reactivos.set(await this.api.reactivos()); } catch(e) { this.error.set(errorText(e)); }
  }
  bajoStock() { return this.reactivos().filter(r => r.cantidadReactivo > 0 && r.cantidadReactivo <= r.stockMinimo); }
  agotados() { return this.reactivos().filter(r => r.cantidadReactivo === 0); }
}
