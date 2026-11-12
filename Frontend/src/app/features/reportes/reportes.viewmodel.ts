import { Injectable, signal } from '@angular/core';
import { ApiService } from '../../core/api.service';
import { Reporte, AlertasResultado } from '../../core/models';
import { errorText } from '../../core/errors';
@Injectable()
export class ReportesViewModel {
  historico = signal<Reporte[]>([]);
  alertas = signal<AlertasResultado | null>(null);
  error = signal('');
  busy = signal(false);
  constructor(private api: ApiService) {}
  async cargar() {
    try {
      this.historico.set(await this.api.reportes());
      this.alertas.set(await this.api.alertas());
      this.error.set('');
    } catch(e) { this.error.set(errorText(e)); }
  }
  async generar(tipo: string, desde: string, hasta: string) {
    if (this.busy()) return;
    this.busy.set(true);
    try {
      const archivo = await this.api.reportePdf(tipo, desde, hasta);
      const url = URL.createObjectURL(archivo);
      const enlace = document.createElement('a');
      enlace.href = url;
      enlace.download = `SGIRQ_${tipo}.pdf`;
      enlace.click();
      setTimeout(() => URL.revokeObjectURL(url), 1000);
      this.error.set('');
      await this.cargar();
    } catch(e) { this.error.set(errorText(e)); }
    finally { this.busy.set(false); }
  }
}
