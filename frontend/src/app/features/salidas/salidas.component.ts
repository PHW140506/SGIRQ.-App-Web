import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../core/auth.service';
import { SalidasViewModel } from './salidas.viewmodel';
@Component({selector:'app-salidas',standalone:true,imports:[FormsModule],
  providers:[SalidasViewModel],templateUrl:'./salidas.component.html'})
export class SalidasComponent implements OnInit {
  idReactivo=0; cantidad=0; idUsuario=1; motivoSalida='';
  fechaSalida = this.fechaLocal();
  desde=''; hasta='';
  // Se mantiene el mismo ID mientras la operación se reintenta.
  idSalida=this.crearId();
  constructor(public vm:SalidasViewModel,public auth:AuthService){}
  ngOnInit(){void this.vm.cargar();if(this.auth.isAdmin())void this.vm.historial();}
  fechaLocal() {
    const now = new Date();
    const local = new Date(now.getTime() - now.getTimezoneOffset() * 60000);
    return local.toISOString().slice(0, 16);
  }
  crearId(){
    const random = new Uint32Array(1);
    crypto.getRandomValues(random);
    return (random[0] % 1800000000) + 100000000;
  }
  async guardar(){
    if(!confirm('¿Confirmas la salida y el descuento del inventario?'))return;
    const ok=await this.vm.registrar({idSalida:this.idSalida,fechaSalida:new Date(this.fechaSalida).toISOString(),
      motivoSalida:this.motivoSalida,idUsuario:this.idUsuario,
      idPersonal:this.auth.session()?.idPersonal,
      reactivos:[{idReactivo:this.idReactivo,cantidadSolicitada:this.cantidad}]});
    if(ok){this.idSalida=this.crearId();this.cantidad=0;this.motivoSalida='';
      if(this.auth.isAdmin())void this.vm.historial(this.desde,this.hasta);}
  }
}
