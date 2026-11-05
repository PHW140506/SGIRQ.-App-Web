import { Injectable, signal } from '@angular/core';
import { ApiService } from '../../core/api.service';
import { Personal } from '../../core/models';
import { errorText } from '../../core/errors';
@Injectable()
export class PersonalViewModel {
  lista = signal<Personal[]>([]); error = signal(''); mensaje = signal('');
  constructor(private api: ApiService) {}
  async cargar(buscar = '') {
    try { this.lista.set(await this.api.personal(buscar)); this.error.set(''); }
    catch (error) { this.error.set(errorText(error)); }
  }
  async guardar(form: any, id: number | null) {
    try {
      if (id) await this.api.editarPersonal(id, {nombrePersonal: form.nombrePersonal,
        apellidosPersonal: form.apellidosPersonal, tipoPersonal: form.tipoPersonal});
      else await this.api.crearPersonal(form);
      this.mensaje.set('Personal guardado correctamente.'); this.error.set('');
      await this.cargar(); return true;
    } catch (error) { this.error.set(errorText(error)); return false; }
  }
}
