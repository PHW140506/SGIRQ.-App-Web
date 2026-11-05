import { Injectable, signal } from '@angular/core';
import { ApiService } from '../../core/api.service';
import { Reactivo } from '../../core/models';
import { errorText } from '../../core/errors';
@Injectable()
export class EntradasViewModel {
  reactivos = signal<Reactivo[]>([]); error = signal(''); mensaje = signal('');
  constructor(private api: ApiService) {}
  async cargar() { try { this.reactivos.set(await this.api.reactivos()); }
    catch(e) { this.error.set(errorText(e)); } }
  async registrar(idReactivo: number, cantidad: number) {
    if (!confirm('¿Confirmas aumentar la existencia del reactivo?')) return false;
    try { const result = await this.api.entrada(idReactivo, cantidad);
      this.mensaje.set(`Entrada registrada. Existencia actual: ${result.cantidadActual}`);
      this.error.set(''); await this.cargar(); return true;
    } catch(e) { this.error.set(errorText(e)); return false; }
  }
}
