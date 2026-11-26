import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { SolicitudesViewModel } from './solicitudes.viewmodel';
@Component({ selector: 'app-solicitudes', standalone: true,
  imports: [FormsModule, RouterLink], providers: [SolicitudesViewModel],
  templateUrl: './solicitudes.component.html' })
export class SolicitudesComponent implements OnInit {
  idUsuario = 0;
  constructor(public vm: SolicitudesViewModel) {}
  ngOnInit() { void this.vm.cargar(); }
}
