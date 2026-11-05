import { Injectable, signal } from '@angular/core';
import { ApiService } from '../../core/api.service';
import { Reactivo, Salida } from '../../core/models';
import { errorText } from '../../core/errors';
@Injectable()
export class SalidasViewModel {
  reactivos=signal<Reactivo[]>([]); salidas=signal<Salida[]>([]);
  error=signal(''); mensaje=signal(''); busy=signal(false);
  constructor(private api:ApiService){}
  async cargar(){try{this.reactivos.set(await this.api.reactivos());}
    catch(e){this.error.set(errorText(e));}}
  async historial(desde='',hasta=''){
    try{this.salidas.set(await this.api.salidas(desde,hasta));this.error.set('');}
    catch(e){this.error.set(errorText(e));}
  }
  async registrar(body:object){
    if(this.busy()) return false;
    this.busy.set(true);
    try{await this.api.salida(body);this.mensaje.set('Salida registrada correctamente.');
      this.error.set('');await this.cargar();return true;}
    catch(e){this.error.set(errorText(e));return false;}
    finally{this.busy.set(false);}
  }
}
