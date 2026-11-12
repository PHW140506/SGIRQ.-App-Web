import { Injectable, signal } from '@angular/core';
import { ApiService } from '../../core/api.service';
import { Reactivo } from '../../core/models';
import { errorText } from '../../core/errors';
@Injectable()
export class InventarioViewModel {
  lista = signal<Reactivo[]>([]); error = signal(''); mensaje = signal('');
  constructor(private api: ApiService) {}
  async cargar(filtros = {}) {
    try { this.lista.set(await this.api.reactivos(filtros)); this.error.set(''); }
    catch(e) { this.error.set(errorText(e)); }
  }
  async guardar(form: any, id: number | null) {
    try {
      if (id) {
        const { cantidadReactivo, idReactivo, ...datos } = form;
        await this.api.editarReactivo(id, datos);
      } else await this.api.crearReactivo(form);
      this.mensaje.set('Reactivo guardado correctamente.');
      await this.cargar(); return true;
    } catch(e) { this.error.set(errorText(e)); return false; }
  }
}
