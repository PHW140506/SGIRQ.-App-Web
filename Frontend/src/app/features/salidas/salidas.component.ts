import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../core/auth.service';
import { SalidasViewModel } from './salidas.viewmodel';
@Component({ selector: 'app-salidas', standalone: true, imports: [FormsModule],
  providers: [SalidasViewModel], templateUrl: './salidas.component.html' })
export class SalidasComponent implements OnInit {
  idUsuario = 0;
  motivoSalida = '';
  fechaSalida = this.fechaLocal();
  desde = '';
  hasta = '';
  items: { idReactivo: number; cantidadSolicitada: number }[] = [{ idReactivo: 0, cantidadSolicitada: 0 }];
  // Se mantiene el folio mientras una petición se reintenta.
  idSalida = this.crearId();
  constructor(public vm: SalidasViewModel, public auth: AuthService) {}
  ngOnInit() { void this.vm.cargar(); if (this.auth.isAdmin()) void this.vm.historial(); }
  agregarItem() { this.items.push({ idReactivo: 0, cantidadSolicitada: 0 }); }
  quitarItem(i: number) { if (this.items.length > 1) this.items.splice(i, 1); }
  fechaLocal(): string {
    const ahora = new Date();
    return new Date(ahora.getTime() - ahora.getTimezoneOffset() * 60000).toISOString().slice(0, 16);
  }
  crearId(): number {
    const valor = new Uint32Array(1);
    crypto.getRandomValues(valor);
    return (valor[0] % 1800000000) + 100000000;
  }
  async guardar() {
    const sesion = this.auth.session();
    if (!sesion || !this.fechaSalida || this.idUsuario <= 0 || !this.motivoSalida.trim()) return;
    if (this.items.some(i => i.idReactivo <= 0 || i.cantidadSolicitada <= 0)) return;
    if (new Set(this.items.map(i => i.idReactivo)).size !== this.items.length) {
      alert('No repitas el mismo reactivo.'); return;
    }
    if (!confirm('¿Confirmas la salida y el descuento del inventario?')) return;
    const ok = await this.vm.registrar({
      idSalida: this.idSalida,
      fechaSalida: new Date(this.fechaSalida).toISOString(),
      motivoSalida: this.motivoSalida.trim(), idUsuario: this.idUsuario,
      idPersonal: sesion.idPersonal, reactivos: this.items
    });
    if (ok) {
      this.idSalida = this.crearId();
      this.items = [{ idReactivo: 0, cantidadSolicitada: 0 }];
      this.motivoSalida = '';
      if (this.auth.isAdmin()) void this.vm.historial(this.desde, this.hasta);
    }
  }
}
