import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../core/auth.service';
import { Reactivo, estados, unidades, anaqueles } from '../../core/models';
import { InventarioViewModel } from './inventario.viewmodel';
@Component({selector:'app-inventario',standalone:true,imports:[FormsModule],
  providers:[InventarioViewModel],templateUrl:'./inventario.component.html'})
export class InventarioComponent implements OnInit {
  estados = estados; unidades = unidades; anaqueles = anaqueles;
  buscar = ''; estadoFisico = ''; colorAnaquel = ''; editarId: number | null = null;
  formulario = this.vacio(); mostrarFormulario = false; detalle: Reactivo | null = null;
  constructor(public vm: InventarioViewModel, public auth: AuthService) {}
  ngOnInit() { void this.cargar(); }
  vacio() { return {nombreComercial:'',nombreQuimico:'',numeroCAS:'',estadoFisico:'Líquido',
    cantidadReactivo:0,unidadMedida:'Mililitro',stockMinimo:0,colorAnaquel:'Verde',
    idPersonal:this.auth?.session()?.idPersonal ?? 0}; }
  cargar() { return this.vm.cargar({buscar:this.buscar, estadoFisico:this.estadoFisico,colorAnaquel:this.colorAnaquel}); }
  limpiarFiltros() { this.buscar = '';this.estadoFisico='';this.colorAnaquel='';void this.cargar(); }
  nuevo() { this.editarId=null; this.formulario=this.vacio(); this.mostrarFormulario=true; }
  editar(r:Reactivo) { this.editarId=r.idReactivo;this.formulario={...r};this.mostrarFormulario=true; }
  async guardar() { if (await this.vm.guardar(this.formulario,this.editarId)) {this.mostrarFormulario=false;void this.cargar();} }
}
