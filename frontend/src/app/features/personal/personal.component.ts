import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PersonalViewModel } from './personal.viewmodel';
import { Personal, roles } from '../../core/models';
@Component({selector: 'app-personal', standalone: true, imports: [FormsModule],
  providers: [PersonalViewModel], templateUrl: './personal.component.html'})
export class PersonalComponent implements OnInit {
  roles = roles; buscar = ''; editId: number | null = null; formulario = this.vacio();
  constructor(public vm: PersonalViewModel) {}
  ngOnInit() { void this.vm.cargar(); }
  vacio() { return {nombrePersonal:'',apellidosPersonal:'',usuarioPersonalLogin:'',contrasenia:'',tipoPersonal:'Auxiliar'}; }
  editar(p: Personal) { this.editId = p.idPersonal; this.formulario = {...this.vacio(), ...p}; }
  limpiar() { this.editId = null; this.formulario = this.vacio(); }
  async guardar() { if (await this.vm.guardar(this.formulario, this.editId)) this.limpiar(); }
}
