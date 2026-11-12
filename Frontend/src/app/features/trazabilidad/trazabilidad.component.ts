import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TrazabilidadViewModel } from './trazabilidad.viewmodel';
@Component({ selector: 'app-trazabilidad', standalone: true, imports: [FormsModule],
  providers: [TrazabilidadViewModel], templateUrl: './trazabilidad.component.html' })
export class TrazabilidadComponent implements OnInit {
  idReactivo = 0;
  constructor(public vm: TrazabilidadViewModel) {}
  ngOnInit() { void this.vm.cargar(); }
}
