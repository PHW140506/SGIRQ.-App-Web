import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EntradasViewModel } from './entradas.viewmodel';
@Component({selector:'app-entradas',standalone:true,imports:[FormsModule],
  providers:[EntradasViewModel],templateUrl:'./entradas.component.html'})
export class EntradasComponent implements OnInit {
  idReactivo=0; cantidad=0;
  constructor(public vm:EntradasViewModel){}
  ngOnInit(){void this.vm.cargar();}
  async guardar(){if(await this.vm.registrar(this.idReactivo,this.cantidad)){this.cantidad=0;}}
}
