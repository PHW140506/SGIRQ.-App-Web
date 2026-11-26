import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReportesViewModel } from './reportes.viewmodel';
@Component({ selector: 'app-reportes', standalone: true, imports: [FormsModule],
  providers: [ReportesViewModel], templateUrl: './reportes.component.html' })
export class ReportesComponent implements OnInit {
  tipo = 'inventario'; desde = ''; hasta = '';
  constructor(public vm: ReportesViewModel) {}
  ngOnInit() { void this.vm.cargar(); }
  generar() { void this.vm.generar(this.tipo, this.desde, this.hasta); }
}
